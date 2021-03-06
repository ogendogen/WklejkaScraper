﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Models.DataParser.Interfaces;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using Database;

namespace WklejkaScraper
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Launching...");
            if (!File.Exists("config.json"))
            {
                Console.WriteLine("File config.json doesn't exist! Stopping");
                Console.ReadKey();
                Environment.Exit(0);
            }

            string configJson = File.ReadAllText("config.json");
            Config config = JsonConvert.DeserializeObject<Config>(configJson);
            
            Crawler crawler = new Crawler(config);
            List<IEntry> entries = new List<IEntry>();

            MongoDb mongoDb = new MongoDb(config.ConnectionString);

            decimal counter = 1;
            decimal maxPages = config.EndPageId - config.StartPageId + 1;
            decimal percentage = 0;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            await foreach (var entry in crawler.Process())
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Processed pages: {counter} / {maxPages}");
                Console.WriteLine($"Processed pages: {percentage.ToString("F")}%");

                await mongoDb.InsertEntry(entry);
                
                counter++;
                percentage = (counter / maxPages) * 100;
            }

            watch.Stop();
            Console.WriteLine("Finished");
            Console.WriteLine($"Time taken: {watch.Elapsed.Hours}h {watch.Elapsed.Minutes}m {watch.Elapsed.Seconds}s");
            Console.ReadKey();
        }
    }
}
