using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.DataParserHandlers;
using Core.Models;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;

namespace Core
{
    internal class DataParser
    {
        public Config Config { get; }

        internal DataParser(Config config)
        {
            Config = config;
        }

        internal IEntry GetEntryByScrapedElements(List<IScrapedElement> scrapedElements, int id)
        {
            IEntry entry = GetContent(scrapedElements, id);
            string author = GetAuthor(scrapedElements);
            DateTime date = GetDate(scrapedElements);

            if (entry is PictureEntry pictureEntry)
            {
                pictureEntry.Author = author;
                pictureEntry.Date = date;
                return pictureEntry;
            }

            if (entry is TextEntry textEntry)
            {
                textEntry.Author = author;
                textEntry.Date = date;
                return textEntry;
            }

            return entry;
        }

        private IEntry GetContent(List<IScrapedElement> scrapedElements, int id)
        {
            var contentElement = scrapedElements.FirstOrDefault(el => el.Name == "Content");
            
            var pictureHandler = new PictureEntryHandler(Config.MaxTriesPerPage);
            var deletedHandler = new DeletedEntryHandler();
            var passwordHandler = new PasswordEntryHandler();
            var errorHandler = new ErrorEntryHandler();
            var textHandler = new TextEntryHandler();

            pictureHandler.SetNext(deletedHandler)
                .SetNext(passwordHandler)
                .SetNext(errorHandler)
                .SetNext(textHandler);

            return pictureHandler.Handle(contentElement, id);
        }

        private DateTime GetDate(List<IScrapedElement> scrapedElements)
        {
            DateTime date = new DateTime();
            var dateElement = scrapedElements.FirstOrDefault(el => el.Name == "Date");
            if (dateElement != null && dateElement is ScrapedTextElement dateTextElement)
            {
                date = ParseDate(dateTextElement);
            }

            return date;
        }

        private string GetAuthor(List<IScrapedElement> scrapedElements)
        {
            string author = String.Empty;
            var authorElement = scrapedElements.FirstOrDefault(el => el.Name == "Author");
            if (authorElement != null && authorElement is ScrapedTextElement authorTextElement)
            {
                author = ParseAuthor(authorTextElement);
            }

            return author;
        }

        private DateTime ParseDate(ScrapedTextElement dateTextElement)
        {
            Match match = Regex.Match(dateTextElement.Content, @"\d{4}-\d{2}-\d{2} \d{2}:\d{2}");
            if (match.Success)
            {
                string matchedValue = match.Value;

                if (DateTime.TryParseExact(matchedValue, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                {
                    return dt;
                }
            }

            return new DateTime();
        }

        private string ParseAuthor(ScrapedTextElement authorTextElement)
        {
            Match match = Regex.Match(authorTextElement.Content, @"~.+\(2");
            if (match.Success)
            {
                string matchedValue = match.Value;
                return matchedValue.TrimStart('~')
                    .TrimEnd('2')
                    .TrimEnd('(')
                    .TrimEnd(' ')
                    .TrimEnd('\'');
            }

            return String.Empty;
        }
    }
}
