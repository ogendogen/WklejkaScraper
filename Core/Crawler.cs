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
        public RequestHandler RequestHandler { get; private set; }
        public Crawler(Config config)
        {
            Config = config;
            RequestHandler = new RequestHandler(Config.Site, Config.MaxTriesPerPage);
        }

        public async Task<IEnumerable<Entry>> Process()
        {
            for (int i=1; i<=Config.PagesAmount; i++)
            {
                var requestResult = await RequestHandler.GetPageContent();
            }
            throw new NotImplementedException();
        }


    }
}
