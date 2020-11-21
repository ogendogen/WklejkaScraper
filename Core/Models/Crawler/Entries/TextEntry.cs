using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Crawler.Interfaces;

namespace Core.Models.Crawler.Entries
{
    public class TextEntry : IEntry
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
