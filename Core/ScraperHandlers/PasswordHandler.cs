﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Abstract;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core.ScraperHandlers
{
    internal class PasswordHandler : ScraperHandler
    {
        public override IScrapedElement Handle(HtmlDocument htmlDoc)
        {
            HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/table/tbody[1]/tr/tbody/tr/td/img");
            if (htmlNode != null)
            {
                if (htmlNode.InnerText.Contains("Podaj hasło:"))
                {
                    return new ScrapedTextElement()
                    {
                        Name = "Content",
                        Content = "password"
                    };
                }

                return new ScrapedTextElement()
                {
                    Name = "Content",
                    Content = "error"
                };
            }
            
            return base.Handle(htmlDoc);
        }
    }
}