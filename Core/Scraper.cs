using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core
{
    internal class Scraper
    {
        internal Scraper()
        {

        }

        internal IEnumerable<IScrapedElement> ScrapAllFromContent(string content)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);

            // todo: chain of responsibility here
            // "Content"
            // text
            HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/tbody[1]/tr/td[2]/pre");
            if (htmlNode == null)
            {
                // picture
                htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/tbody[2]/tr/td/img");
                if (htmlNode == null)
                {
                    // password
                    htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/form");
                    if (htmlNode == null)
                    {
                        // deleted
                        htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/div[1]/form/fieldset/div[3]/div/textarea");
                        if (htmlNode == null)
                        {
                            yield return new ScrapedTextElement()
                            {
                                Name = "Content",
                                Content = "error"
                            };
                        }
                        else
                        {
                            yield return new ScrapedTextElement()
                            {
                                Name = "Content",
                                Content = "deleted"
                            };
                        }
                    }
                    else
                    {
                        if (htmlNode.InnerText.Contains("Podaj hasło:"))
                        {
                            yield return new ScrapedTextElement()
                            {
                                Name = "Content",
                                Content = "password"
                            };
                        }
                        else
                        {
                            yield return new ScrapedTextElement()
                            {
                                Name = "Content",
                                Content = "error"
                            };
                        }
                    }
                }
                else
                {
                    yield return new ScrapedPictureElement()
                    {
                        Name = "Content",
                        Path = htmlNode.GetAttributeValue("src", "empty picture")
                    };
                }

            }
            else
            {
                yield return new ScrapedTextElement()
                {
                    Name = "Content",
                    Content = htmlNode.InnerText
                };
            }

            // "Author" and "Date"
            htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/thead/tr/td/table/tbody/tr/td[1]");
            if (htmlNode != null)
            {
                yield return new ScrapedTextElement()
                {
                    Name = "Author",
                    Content = htmlNode.InnerText
                };

                yield return new ScrapedTextElement()
                {
                    Name = "Date",
                    Content = htmlNode.InnerText
                };
            }
        }
    }
}
