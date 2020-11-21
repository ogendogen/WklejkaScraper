using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Models;
using Core.Models.Crawler.Entries;
using Core.Models.Crawler.Interfaces;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;

namespace Core
{
    internal class DataParser
    {
        internal Config Config { get; set; }
        public DataParser(Config config)
        {
            Config = config;
        }

        internal IEntry GetEntryByScrapedElement(IScrapedElement scrapedElement, int id)
        {
            string author = String.Empty;
            string content = String.Empty;
            DateTime date = new DateTime();

            if (scrapedElement.Name == "Author" && scrapedElement is ScrapedTextElement textElement)
            {
                author = textElement.Content;
            }
            else if (scrapedElement.Name == "Content" && scrapedElement is ScrapedTextElement textElement2)
            {
                content = textElement2.Content;
            }
            else if (scrapedElement.Name == "Date" && scrapedElement is ScrapedDateElement dateElement)
            {
                date = dateElement.Content;
            }

            // todo: refactor me please
            if (content.Contains("wklejka/wyswietlImage.php"))
            {
                return new PictureEntry()
                {
                    ID = id,
                    Author = author,
                    Date = date,
                    PicturePath = content,
                    Picture = new byte[1] { 0 },
                    ReadPicture = "soon"
                };
            }
            else if (content.Contains("Podaj hasło:"))
            {
                return new PasswordProtectedEntry()
                {
                    ID = id
                };
            }
            else
            {
                return new TextEntry()
                {
                    ID = id,
                    Author = author,
                    Content = content,
                    Date = date
                };
            }
        }
    }
}
