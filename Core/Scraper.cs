﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core
{
    public class Scraper
    {
        public List<ElementToScrap> ElementsToScrap { get; set; }
        public Scraper(List<ElementToScrap> elementsToScrap)
        {
            ElementsToScrap = elementsToScrap;
        }

        public IEnumerable<IScrapedElement> ScrapAllFromContent(string content)
        {
            foreach (var element in ElementsToScrap)
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(content);

                HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode(element.Path);
                string parsedElement = ParseContent(htmlNode, element.Regex);
                if (element.Name.Contains("Date"))
                {
                    if (DateTime.TryParse(parsedElement, out DateTime dt))
                    {
                        yield return new ScrapedDateElement()
                        {
                            Name = element.Name,
                            Content = dt
                        };
                    }
                    else
                    {
                        yield return new ScrapedTextElement()
                        {
                            Name = element.Name,
                            Content = parsedElement
                        };
                    }
                }
                else
                {
                    yield return new ScrapedTextElement()
                    {
                        Name = element.Name,
                        Content = parsedElement
                    };
                }
            }
        }

        private string ParseContent(HtmlNode htmlNode, string regex="")
        {
            string innerText = htmlNode.InnerText;
            if (String.IsNullOrEmpty(regex))
            {
                return innerText;
            }

            Match match = Regex.Match(innerText, regex);
            if (match.Success)
            {
                return match.Value;
            }

            return innerText;
        }
    }
}