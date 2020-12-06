using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.DataParser.Interfaces;

namespace Core.Models.DataParser.Entries
{
    public class TextEntry : IEntry
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
