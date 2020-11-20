using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Entry
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public bool IsSucceded { get; set; }
    }
}
