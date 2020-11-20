using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Scraper;

namespace Core
{
    public class Crawler
    {
        public Config Config { get; private set; }
        public RequestHandler RequestHandler { get; private set; }
        public Scraper Scraper { get; set; }
        public Crawler(Config config)
        {
            Config = config;
            Configuration(config);
        }

        public async Task<IEnumerable<Entry>> Process()
        {
            for (int i=1; i<=Config.PagesAmount; i++)
            {
                var requestResult = await RequestHandler.GetPageContent();
            }
            throw new NotImplementedException();
        }

        private void Configuration(Config config)
        {
            RequestHandler = new RequestHandler(Config.Site, Config.MaxTriesPerPage);
            Scraper = new Scraper(Config.ElementsToScrap);
        }
    }
}
