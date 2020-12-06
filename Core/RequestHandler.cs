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
    }
}
