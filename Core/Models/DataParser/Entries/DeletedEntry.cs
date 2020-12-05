using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.DataParser.Interfaces;

namespace Core.Models.DataParser.Entries
{
    public class DeletedEntry : IEntry
    {
        public int ID { get; set; }
    }
}
