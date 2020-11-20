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
        public Config Config { get; set; }
        public Crawler(Config config)
        {
            Config = config;
        }

        public List<Entry> Process()
        {
            throw new NotImplementedException();
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
