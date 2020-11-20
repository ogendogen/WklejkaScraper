using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Models.Scraper;
using HtmlAgilityPack;

namespace Core
{
    public class Scraper
    {
        public List<ElementToScrap> ElementsToScrap { get; set; }
        public Scraper(params ElementToScrap[] elements)
        {
            foreach (var element in elements)
            {
                ElementsToScrap.Add(element);
            }
        }

        public IEnumerable<ScrapedElement> ScrapFromContent(string content)
        {
            foreach (var element in ElementsToScrap)
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(content);

                HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode(element.Path);
                string parsedElement = ParseContent(htmlNode, element.Regex);

                yield return new ScrapedElement()
                {
                    Name = element.Name,
                    Content = parsedElement
                };
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
