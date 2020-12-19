using System;
using System.Collections.Generic;
using System.Text;
using Database.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Models
{
    public class FailedDoc : IDoc
    {
        public int PageID { get; set; }
        [BsonIgnoreIfDefault]
        public int StatusCode { get; set; }
        [BsonIgnoreIfDefault]
        public string StackTrace { get; set; }
    }
}
