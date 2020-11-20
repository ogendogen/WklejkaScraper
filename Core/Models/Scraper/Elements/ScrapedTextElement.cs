using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Interfaces;

namespace Core.Models.Scraper.Elements
{
    public class ScrapedTextElement : IScrapedElement
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
