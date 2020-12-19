using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;

namespace Core.Models
{
    public class Config
    {
        public int MaxTriesPerPage { get; set; }
        public int StartPageId { get; set; }
        public int EndPageId { get; set; }
        public string OCRApiKey { get; set; }
        public string ConnectionString { get; set; }
    }
}
