﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;
using System.Threading;

namespace Core
{
    public class Crawler
    {
        public Config Config { get; private set; }
        internal RequestHandler RequestHandler { get; private set; }
        internal Scraper Scraper { get; set; }
        internal DataParser DataParser { get; set; }
        public List<IEntry> ProcessedEntries { get; set; }

        private object _locker = new object();
        //private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private List<ManualResetEvent> _resetEvents;
        public Crawler(Config config)
        {
            Config = config;
            Configuration();
            ProcessedEntries = new List<IEntry>();
            _resetEvents = new List<ManualResetEvent>();
        }

        public void Process()
        {
            //ParallelLoopResult parallelResult = Parallel.For(Config.StartPageId, Config.EndPageId, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, i =>  // await GetEntry(i));
            //{
            //    lock(_locker)
            //    {
            //        ProcessedEntries.Add(GetEntry(i));
            //    }
            //});

            //await Task.WhenAll(ProcessedEntries);
            //ThreadPool.SetMaxThreads(4, 4);
            
            //for (int i = Config.StartPageId; i <= Config.EndPageId; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(async state => await GetEntry(i)));
            //}

            Barrier barrier = new Barrier(Config.EndPageId - Config.StartPageId + 1);
            for (int i = Config.StartPageId; i <= Config.EndPageId; i++)
            {
                //ThreadPool.QueueUserWorkItem(new WaitCallback(async state => await GetEntry(i)));
                ThreadPool.QueueUserWorkItem(
                    async arg =>
                    {
                        int pageNumber = (int)arg;
                        Console.WriteLine("Queued ID " + pageNumber);
                        var entry = await GetEntry(pageNumber);
                        Console.WriteLine("Done ID " + pageNumber);
                        ProcessedEntries.Add(entry);
                        barrier.SignalAndWait();
                    }, i);
            }
            barrier.SignalAndWait();
        }

        private async Task<IEntry> GetEntry(int i)
        {
            try
            {
                var requestResult = await RequestHandler.GetPageContent(i);
                if (requestResult.StatusCode == 200)
                {
                    var scrapedElements = Scraper.ScrapAllFromContent(requestResult.Content).ToList();
                    return DataParser.GetEntryByScrapedElements(scrapedElements, i);
                }
                else
                {
                    return new FailedEntry()
                    {
                        ID = i,
                        StatusCode = requestResult.StatusCode
                    };
                }
            }
            catch (Exception ex)
            {
                return new FailedEntry()
                {
                    ID = i,
                    StackTrace = ex.StackTrace
                };
            }
        }

        private void Configuration()
        {
            RequestHandler = new RequestHandler(Config.MaxTriesPerPage);
            Scraper = new Scraper();
            DataParser = new DataParser(Config);
            OCR.ApiKey = Config.OCRApiKey;
        }
    }
}
