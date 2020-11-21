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

                    // todo: refactor me please
                    if (content.Contains("wklejka/wyswietlImage.php"))
                    {
                        yield return new PictureEntry()
                        {
                            ID = i,
                            Author = author,
                            Date = date,
                            PicturePath = content,
                            Picture = new byte[1] { 0 },
                            ReadPicture = "soon"
                        };
                    }
                    else if (content.Contains("Podaj hasło:"))
                    {
                        yield return new PasswordProtectedEntry()
                        {
                            ID = i
                        };
                    }
                    else
                    {
                        yield return new TextEntry()
                        {
                            ID = i,
                            Author = author,
                            Content = content,
                            Date = date
                        };
                    }
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
