using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Core.Models.Scraper.Abstract;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core.ScraperHandlers
{
    internal class AuthorHandler : ScraperHandler
    {
        public override IScrapedElement Handle(HtmlDocument htmlDoc, string name)
        {
            HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/thead/tr/td/table/tr/td[1]");
            if (htmlNode != null)
            {
                return new ScrapedTextElement()
                {
                    Name = name,
                    Content = WebUtility.HtmlDecode(htmlNode.InnerText)
                };
            }
            
            return base.Handle(htmlDoc, name);
        }
    }
}
