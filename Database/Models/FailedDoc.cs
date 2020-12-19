using System;
using System.Collections.Generic;
using System.Text;
using Database.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Models
{
    public class FailedDoc : IDoc
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public int ID { get; set; }
        public int PageID { get; set; }
        public int StatusCode { get; set; }
        public string StackTrace { get; set; }
    }
}
