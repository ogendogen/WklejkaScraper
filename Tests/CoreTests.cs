using Core;
using Core.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Core.Models.Scraper.Elements;
using System.Threading.Tasks;
using Core.Models.Crawler.Interfaces;
using Core.Models.Crawler.Entries;

namespace Tests
{
    public class CoreTests
    {
        public static IEnumerable<Config> SinglePageConfig
        {
            get
            {
                yield return new Config() 
                { 
                    StartPageId = 870065,
                    EndPageId = 870065,
                    MaxTriesPerPage = 10
                };
            }
        }

        [Test]
        [TestCaseSource("SinglePageConfig")]
        public async Task SinglePageTest(Config config)
        {
            Crawler crawler = new Crawler(config);
            await foreach (var entry in crawler.Process())
            {
                if (entry is TextEntry textEntry)
                {
                    Assert.AreEqual(textEntry.Content, "test1\ntest2\ntest3");
                }
                else
                {
                    Assert.Fail("Entry not text");
                }
            }
        }

        public static IEnumerable<Config> MultiplePagesConfig
        {
            get
            {
                yield return new Config()
                {
                    StartPageId = 1,
                    EndPageId = 5,
                    MaxTriesPerPage = 10
                };
            }
        }

        [Test]
        [TestCaseSource("MultiplePagesConfig")]
        public async Task MultiplePagesTest(Config config)
        {
            Crawler crawler = new Crawler(config);
            List<IEntry> entries = new List<IEntry>();

            await foreach (var entry in crawler.Process())
            {
                entries.Add(entry);
            }

            Assert.Fail("failed test - need to rebuild something");
        }
    }
}