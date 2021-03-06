﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    internal class RequestHandler
    {
        internal int MaxTries { get; private set; }
        internal RequestHandler(int maxTries)
        {
            MaxTries = maxTries;
        }

        internal async Task<RequestResult> GetPageContent(int pageId)
        {
            int maxTriesTmp = MaxTries;
            RequestResult requestResult = new RequestResult();
            while (maxTriesTmp > 0)
            {
                requestResult = await RequestPageContent(pageId);
                if (requestResult.StatusCode == 200)
                {
                    return new RequestResult() { StatusCode = 200, Content = requestResult.Content};
                }
                else if (requestResult.StatusCode == 404)
                {
                    return new RequestResult() { StatusCode = 404, Content = ""};
                }
                maxTriesTmp--;
            }
            return requestResult;
        }

        internal byte[] GetImage(string imageUrl)
        {
            int maxTriesTmp = MaxTries;
            while (maxTriesTmp > 0)
            {
                var byteRequestResult = RequestImage(imageUrl);
                if (byteRequestResult.StatusCode == 200)
                {
                    return byteRequestResult.Content;
                }
                maxTriesTmp--;
            }
            
            return new byte[1] { 0 };
        }

        private async Task<RequestResult> RequestPageContent(int pageId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://wklejto.pl/{pageId}");
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0";
                using (var response = await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;
                    string contents = await reader.ReadToEndAsync();

                    return new RequestResult() { StatusCode = (int)statusCode, Content = contents};
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    return new RequestResult()
                    {
                        StatusCode = -1,
                        Content = e.Message
                    };
                }

                HttpWebResponse response = (HttpWebResponse)e.Response;
                return new RequestResult() { StatusCode = (int)response.StatusCode};
            }
        }

        private ByteRequestResult RequestImage(string imageUrl)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0");
                    byte[] imageBytes = webClient.DownloadData($"http://wklej.to/{imageUrl}");
                    
                    return new ByteRequestResult()
                    {
                        Content = imageBytes,
                        StatusCode = 200
                    };
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    return new ByteRequestResult()
                    {
                        StatusCode = -1,
                        Content = new byte[1]{ 0 }
                    };
                }

                HttpWebResponse response = (HttpWebResponse)e.Response;
                return new ByteRequestResult()
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }
    }
}
