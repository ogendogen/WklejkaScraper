using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;

namespace Core.Models
{
    public class Config
    {
        public string Site { get; set; }
        public List<ElementToScrap> ElementsToScrap { get; set; }
        public string VariableSymbol { get; set; }
        public int PagesAmount { get; set; }
        public int MaxTriesPerPage { get; set; }
        public Config()
        {
            ElementsToScrap = new List<ElementToScrap>();
        }
    }
}
