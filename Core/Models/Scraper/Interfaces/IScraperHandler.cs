using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Core.Models.Scraper.Interfaces
{
    interface IScraperHandler
    {
        IScraperHandler SetNext(IScraperHandler handler);
        IScrapedElement Handle(HtmlDocument request);
    }
}
