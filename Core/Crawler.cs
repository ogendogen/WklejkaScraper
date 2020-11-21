using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Crawler.Entries;
using Core.Models.Crawler.Interfaces;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;

namespace Core
{
    public class Crawler
    {
        public Config Config { get; private set; }
        internal RequestHandler RequestHandler { get; private set; }
        internal Scraper Scraper { get; set; }
        public Crawler(Config config)
        {
            Config = config;
            Configuration();
        }

        public async IAsyncEnumerable<IEntry> Process()
        {
            for (int i=1; i<=Config.PagesAmount; i++)
            {
                var requestResult = await RequestHandler.GetPageContent();
                if (requestResult.StatusCode == 200)
                {
                    var scrapedElements = Scraper.ScrapAllFromContent(requestResult.Content).ToList();
                    
                    
                }
                else
                {
                    yield return new FailedEntry()
                    {
                        ID = i,
                        StatusCode = requestResult.StatusCode
                    };
                }
            }
        }

        private void Configuration()
        {
            RequestHandler = new RequestHandler(Config.Site, Config.MaxTriesPerPage);
            Scraper = new Scraper(Config.ElementsToScrap);
        }
    }
}
