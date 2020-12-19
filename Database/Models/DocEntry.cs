using System;
using Database.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Models
{
    public class DocEntry : IDoc
    {
        public int PageID { get; set; }
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string Author { get; set; }
        [BsonIgnoreIfDefault]
        public DateTime Date { get; set; }
        [BsonIgnoreIfDefault]
        public string Content { get; set; }
        [BsonIgnoreIfDefault]
        public string PicturePath { get; set; }
        [BsonIgnoreIfNull]
        public byte[] Picture { get; set; }
        [BsonIgnoreIfDefault]
        public bool IsPasswordProtected { get; set; }
    }
}