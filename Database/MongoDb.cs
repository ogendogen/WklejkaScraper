using System;
using Database.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Core;

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
    }
}
