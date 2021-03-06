﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Abstract;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core.ScraperHandlers
{
    internal class DeletedHandler : ScraperHandler
    {
        public override IScrapedElement Handle(HtmlDocument htmlDoc, string name)
        {
            HtmlNode htmlNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[3]/div[1]/form/fieldset/div[3]/div/textarea");
            if (htmlNode != null)
            {
                return new ScrapedTextElement()
                {
                    Name = name,
                    Content = "deleted"
                };
            }

            return base.Handle(htmlDoc, name);
        }
    }
}
