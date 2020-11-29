using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Abstract;
using Core.Models.Scraper.Elements;
using HtmlAgilityPack;

namespace Core.ScraperHandlers
{
    internal class TextHandler : ScraperHandler
    {
        public override object Handle(object request)
        {
            HtmlDocument htmlDoc = (HtmlDocument)request;
            HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/tbody[1]/tr/td[2]/pre");
            if (htmlNode != null)
            {
                return new ScrapedTextElement()
                {
                    Name = "Content",
                    Content = htmlNode.InnerText
                };
            }
            
            return base.Handle(request);
        }
    }
}
