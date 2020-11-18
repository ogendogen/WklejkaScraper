using System;
using System.Collections.Generic;
using Core.Models;

namespace Core
{
    public class Crawler
    {
        public Config Config { get; set; }
        public Crawler(Config config)
        {
            Config = config;
        }

        public List<Entry> Process()
        {
            throw new NotImplementedException();
        }
    }
}
