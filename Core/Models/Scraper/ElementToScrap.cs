using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Scraper
{
    public class ElementToScrap
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Regex { get; set; }
    }
}
