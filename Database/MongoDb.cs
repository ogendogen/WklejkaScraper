using System;
using Database.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Core.Models.DataParser.Interfaces;
using Core.Models.DataParser.Entries;
using Database.Interfaces;

namespace Database
{
    public class MongoDb
    {
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase MongoDatabase { get; set; }
        public IMongoCollection<DocEntry> DocsCollection { get; set; }
        public IMongoCollection<FailedDoc> FailedDocsCollection { get; set; }
        
        public MongoDb(string connectionString)
        {
            MongoClient = new MongoClient(connectionString);
            
            MongoDatabase = MongoClient.GetDatabase("wklejto");
            DocsCollection = MongoDatabase.GetCollection<DocEntry>("docs");
            FailedDocsCollection = MongoDatabase.GetCollection<FailedDoc>("failed_docs");
        }

        public void InsertEntry(IEntry entry)
        {
            IDoc doc = MapEntryToDoc(entry);
        }

        private IDoc MapEntryToDoc(IEntry entry)
        {
            if (entry is FailedEntry failedEntry)
            {
                return new FailedDoc()
                {
                    PageID = failedEntry.ID,
                    StatusCode = failedEntry.StatusCode,
                    StackTrace = failedEntry.StackTrace
                };
            }

            if (entry is PictureEntry pictureEntry)
            {
                return new DocEntry()
                {
                    PageID = pictureEntry.ID,
                    Author = pictureEntry.Author,
                    Date = pictureEntry.Date,
                    Content = pictureEntry.OCRResponse,
                    Picture = pictureEntry.Picture,
                    PicturePath = pictureEntry.PicturePath
                };
            }
            else if (entry is TextEntry textEntry)
            {
                return new DocEntry()
                {
                    PageID = textEntry.ID,
                    Author = textEntry.Author,
                    Date = textEntry.Date,
                    Content = textEntry.Content
                };
            }

            return new DocEntry()
            {
                PageID = entry.ID
            };
        }
    }
}
