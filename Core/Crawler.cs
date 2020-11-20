using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;

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
            Configuration();
        }

        public async IAsyncEnumerable<Entry> Process()
        {
            for (int i=1; i<=Config.PagesAmount; i++)
            {
                var requestResult = await RequestHandler.GetPageContent();
                if (requestResult.StatusCode == 200)
                {
                    var scrapedElements = Scraper.ScrapAllFromContent(requestResult.Content).ToList();
                    
                    string author = String.Empty;
                    string content = String.Empty;
                    DateTime date = new DateTime();

                    if (scrapedElements.FirstOrDefault(el => el.Name == "Author") is ScrapedTextElement textElement)
                    {
                        author = textElement.Content;
                    }
                    if (scrapedElements.FirstOrDefault(el => el.Name == "Content") is ScrapedTextElement textElement2)
                    {
                        content = textElement2.Content;
                    }
                    if (scrapedElements.FirstOrDefault(el => el.Name == "Date") is ScrapedDateElement dateElement)
                    {
                        date = dateElement.Content;
                    }

                    yield return new Entry()
                    {
                        ID = i,
                        IsSuccessed = true,
                        Author = author,
                        Content = content,
                        Date = date
                    };
                }
                else
                {
                    yield return new Entry()
                    {
                        ID = i,
                        IsSuccessed = false,
                        Content = "error"
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
