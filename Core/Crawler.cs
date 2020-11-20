using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public class Crawler
    {
        public Config Config { get; private set; }
        public Crawler(Config config)
        {
            Config = config;
        }

        public async Task<IEnumerable<Entry>> Process()
        {
            for (int i=1; i<=Config.PagesAmount; i++)
            {
                var requestResult = await GetPageContent(Config.Site, Config.MaxTriesPerPage);

            }
            throw new NotImplementedException();
        }

        private async Task<RequestResult> GetPageContent(string page, int maxTries)
        {
            RequestResult requestResult = new RequestResult();
            while (maxTries > 0)
            {
                requestResult = await RequestPageContent(page);
                if (requestResult.StatusCode == 200)
                {
                    return new RequestResult() { StatusCode = 200, Content = requestResult.Content};
                }
                maxTries--;
            }
            return requestResult;
        }

        private async Task<RequestResult> RequestPageContent(string page)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(page);
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
