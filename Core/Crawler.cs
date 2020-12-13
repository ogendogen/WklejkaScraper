using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;
using System.Threading;
using System.Diagnostics;

namespace Core
{
    public class Crawler
    {
        public Config Config { get; private set; }
        internal RequestHandler RequestHandler { get; private set; }
        internal Scraper Scraper { get; set; }
        internal DataParser DataParser { get; set; }
        public Crawler(Config config)
        {
            Config = config;
            Configuration();
        }

        public async IAsyncEnumerable<IEntry> Process()
        {
            for (int i=Config.StartPageId; i<=Config.EndPageId; i++)
            {
                var requestResult = await RequestHandler.GetPageContent(i);
                if (requestResult.StatusCode == 200)
                {
                    var scrapedElements = Scraper.ScrapAllFromContent(requestResult.Content).ToList();
                    yield return DataParser.GetEntryByScrapedElements(scrapedElements, i);
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
            RequestHandler = new RequestHandler(Config.MaxTriesPerPage);
            Scraper = new Scraper();
            DataParser = new DataParser(Config);
            OCR.ApiKey = Config.OCRApiKey;
        }
    }
}
