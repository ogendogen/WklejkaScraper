using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.DataParser.Interfaces;

namespace Core.Models.DataParser.Entries
{
    public class FailedEntry : IEntry
    {
        public int ID { get; set; }
        public int StatusCode { get; set; }
        public string StackTrace { get; set; }
    }
}
