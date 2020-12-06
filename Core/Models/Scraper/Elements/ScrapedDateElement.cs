using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Interfaces;

namespace Core.Models.Scraper.Elements
{
    public class ScrapedDateElement : IScrapedElement
    {
        public string Name { get; set; }
        public DateTime Content { get; set; }
    }
}
