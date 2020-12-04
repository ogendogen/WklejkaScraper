using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Abstract;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core.ScraperHandlers
{
    internal class PictureHandler : ScraperHandler
    {
        public override IScrapedElement Handle(HtmlDocument htmlDoc, string name)
        {
            HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/tbody[1]/tr/tbody/tr/td/img");
            if (htmlNode != null)
            {
                return new ScrapedPictureElement()
                {
                    Name = name,
                    Path = htmlNode.GetAttributeValue("src", "empty picture")
                };
            }
            
            return base.Handle(htmlDoc, name);
        }
    }
}
