using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public class RequestHandler
    {
        public string Page { get; private set; }
        public int MaxTries { get; private set; }
        public RequestHandler(string page, int maxTries)
        {
            Page = page;
            MaxTries = maxTries;
        }

        public async Task<RequestResult> GetPageContent()
        {
            RequestResult requestResult = new RequestResult();
            while (MaxTries > 0)
            {
                requestResult = await RequestPageContent();
                if (requestResult.StatusCode == 200)
                {
                    return new RequestResult() { StatusCode = 200, Content = requestResult.Content};
                }
                MaxTries--;
            }
            return requestResult;
        }

        private async Task<RequestResult> RequestPageContent()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Page);
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
    }
}
