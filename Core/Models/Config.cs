﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;

namespace Core.Models
{
    public class Config
    {
        public List<ElementToScrap> ElementsToScrap { get; set; }
        public int MaxTriesPerPage { get; set; }
        public int StartPageId { get; set; }
        public int EndPageId { get; set; }
        public Config()
        {
            ElementsToScrap = new List<ElementToScrap>();
        }
    }
}
