using System;
using Database.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Models
{
    public class DocEntry : IDoc
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public int ID { get; set; }
        public int PageID { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string PicturePath { get; set; }
        public byte[] Picture { get; set; }
    }
}