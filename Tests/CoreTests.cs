using Core;
using Core.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Core.Models.Scraper.Elements;
using System.Threading.Tasks;

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
                    Site = "http://wklejto.pl/870065",
                    PagesAmount = 1,
                    MaxTriesPerPage = 10,
                    ElementsToScrap = new List<ElementToScrap>() { new ElementToScrap() 
                        { Name = "Content", Path = "/html/body/div[2]/div[3]/table/tbody[1]/tr/td[2]/pre"}}
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
                Assert.AreEqual(entry.Content, "test1\ntest2\ntest3");
            }
        }

        public static IEnumerable<Config> MultiplePagesConfig
        {
            get
            {
                yield return new Config()
                {
                    Site = "http://wklejto.pl/X",
                    PagesAmount = 5,
                    MaxTriesPerPage = 10,
                    ElementsToScrap = new List<ElementToScrap>() { new ElementToScrap() 
                        { Name = "Content", Path = "/html/body/div[2]/div[3]/table/tbody[1]/tr/td[2]/pre"}},
                    VariableSymbol = "X"
                };
            }
        }

        [Test]
        [TestCaseSource("MultiplePagesConfig")]
        public async Task MultiplePagesTest(Config config)
        {
            Crawler crawler = new Crawler(config);
            List<Entry> entries = new List<Entry>();

            await foreach (var entry in crawler.Process())
            {
                entries.Add(entry);
            }

            Assert.Fail("failed test - need to rebuild something");
        }
    }
}