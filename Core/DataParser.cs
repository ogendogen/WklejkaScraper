using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Models;
using Core.Models.Crawler.Entries;
using Core.Models.Crawler.Interfaces;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;

namespace Core
{
    internal class DataParser
    {
        internal DataParser()
        {

        }

        internal IEntry GetEntryByScrapedElements(List<IScrapedElement> scrapedElements, int id)
        {
            string content = String.Empty;
            var contentElement = scrapedElements.FirstOrDefault(el => el.Name == "Content");
            if (contentElement != null)
            {
                // determine type of content
                if (contentElement is ScrapedPictureElement pictureElement)
                {
                    return new PictureEntry()
                    {
                        ID = id,
                        PicturePath = pictureElement.Path
                        // todo: OCR here
                    };
                }
                else if (contentElement is ScrapedTextElement textElement)
                {
                    if (textElement.Content == "deleted")
                    {
                        return new DeletedEntry()
                        {
                            ID = id
                        };
                    }
                    else if (textElement.Content == "error")
                    {
                        return new FailedEntry()
                        {
                            ID = id,
                            StatusCode = -1
                        };
                    }
                    else if (textElement.Content == "password")
                    {
                        return new PasswordProtectedEntry()
                        {
                            ID = id
                        };
                    }
                    else
                    {
                        content = textElement.Content;
                    }
                }
            }

            string author = String.Empty;
            var authorElement = scrapedElements.FirstOrDefault(el => el.Name == "Author");
            if (authorElement != null && authorElement is ScrapedTextElement authorTextElement)
            {
                author = ParseAuthor(authorTextElement);
            }

            DateTime date = new DateTime();
            var dateElement = scrapedElements.FirstOrDefault(el => el.Name == "Date");
            if (dateElement != null && dateElement is ScrapedTextElement dateTextElement)
            {
                date = ParseDate(dateTextElement);
            }

            return new TextEntry()
            {
                ID = id,
                Author = author,
                Date = date,
                Content = content
            };
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
