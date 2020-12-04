using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Abstract;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core.ScraperHandlers
{
    internal class AuthorHandler : ScraperHandler
    {
        public override IScrapedElement Handle(HtmlDocument htmlDoc)
        {
            HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/thead/tr/td/table/tbody/tr/td[1]");
            if (htmlNode != null)
            {
                return new ScrapedTextElement()
                {
                    Name = "Content",
                    Content = htmlNode.InnerText
                };
            }
            
            return base.Handle(htmlDoc);
        }
    }
}
