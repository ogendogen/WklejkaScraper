using System;
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
            RequestResult requestResult = new RequestResult();
            while (MaxTries > 0)
            {
                requestResult = await RequestPageContent(pageId);
                if (requestResult.StatusCode == 200)
                {
                    return new RequestResult() { StatusCode = 200, Content = requestResult.Content};
                }
                MaxTries--;
            }
            return requestResult;
        }

        internal byte[] GetImage(string imageUrl)
        {
            while (MaxTries > 0)
            {
                var byteRequestResult = RequestImage(imageUrl);
                if (byteRequestResult.StatusCode == 200)
                {
                    return byteRequestResult.Content;
                }
                MaxTries--;
            }
            
            return new byte[1] { 0 };
        }

        private async Task<RequestResult> RequestPageContent(int pageId)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://wklejto.pl/{pageId}");
                request.Method = "GET";
                using (var response = await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;
                    string contents = reader.ReadToEnd();

                    return new RequestResult() { StatusCode = (int)statusCode, Content = contents};
                }
            }
            catch (WebException e)
            {
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
                    byte[] imageBytes = webClient.DownloadData(imageUrl);
                    
                    return new ByteRequestResult()
                    {
                        Content = imageBytes,
                        StatusCode = 200
                    };
                }
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                return new ByteRequestResult()
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }
    }
}
