using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Config
    {
        public string Site { get; set; }
        public string AuthorPath { get; set; }
        public string DatePath { get; set; }
        public string ContentPath { get; set; }
        public string VariableSymbol { get; set; }
        public int PagesAmount { get; set; }
        public int MaxTriesPerPage { get; set; }
    }
}
