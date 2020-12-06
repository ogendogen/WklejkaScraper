using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Interfaces;

namespace Core.Models.Scraper.Elements
{
    public class ScrapedPictureElement : IScrapedElement
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
