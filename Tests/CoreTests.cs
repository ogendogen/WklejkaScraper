using Core;
using Core.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Core.Models.Scraper.Elements;
using System.Threading.Tasks;
using Core.Models.DataParser.Interfaces;
using Core.Models.DataParser.Entries;
using System.Linq;

namespace Tests
{
    public class CoreTests
    {
        //public static IEnumerable<Config> SinglePageConfig
        //{
        //    get
        //    {
        //        yield return new Config() 
        //        { 
        //            StartPageId = 870065,
        //            EndPageId = 870065,
        //            MaxTriesPerPage = 10
        //        };
        //    }
        //}

        //[Test]
        //[TestCaseSource("SinglePageConfig")]
        //public async Task SinglePageTest(Config config)
        //{
        //    Crawler crawler = new Crawler(config);
        //    await foreach (var entry in crawler.Process())
        //    {
        //        if (entry is TextEntry textEntry)
        //        {
        //            Assert.AreEqual(textEntry.Content, "test1\ntest2\ntest3");
        //        }
        //        else
        //        {
        //            Assert.Fail("Entry not text");
        //        }
        //    }
        //}

        //public static IEnumerable<Config> MultiplePagesConfig
        //{
        //    get
        //    {
        //        yield return new Config()
        //        {
        //            StartPageId = 1,
        //            EndPageId = 5,
        //            MaxTriesPerPage = 10
        //        };
        //    }
        //}

        //[Test]
        //[TestCaseSource("MultiplePagesConfig")]
        //public async Task MultiplePagesTest(Config config)
        //{
        //    Crawler crawler = new Crawler(config);
        //    List<IEntry> entries = new List<IEntry>();

        //    await foreach (var entry in crawler.Process())
        //    {
        //        entries.Add(entry);
        //    }

        //    Assert.AreEqual(entries.Count, 5);

        //    Assert.IsInstanceOf(typeof(DeletedEntry), entries[0]);

        //    Assert.IsInstanceOf(typeof(TextEntry), entries[1]);
        //    TextEntry page2 = (TextEntry)entries[1];
        //    StringAssert.Contains("TwojePC.pl | PC | Recenzje | Testy | Newsy | Download | Gry | Pliki - TWOJEPC", page2.Content);

        //    Assert.IsInstanceOf(typeof(DeletedEntry), entries[2]);

        //    Assert.IsInstanceOf(typeof(PictureEntry), entries[3]);
        //    PictureEntry page4 = (PictureEntry)entries[3];
        //    Assert.AreEqual(page4.PicturePath, "wklejka/wyswietlImage.php?id=51662&par=gif&9204");

        //    Assert.IsInstanceOf(typeof(TextEntry), entries[4]);
        //    TextEntry page5 = (TextEntry)entries[4];
        //    StringAssert.Contains("testowy blalbablalbl", page5.Content);
        //}

        //public static IEnumerable<Config> PasswordPageConfig
        //{
        //    get
        //    {
        //        yield return new Config()
        //        {
        //            StartPageId = 16,
        //            EndPageId = 16,
        //            MaxTriesPerPage = 10
        //        };
        //    }
        //}

        //[Test]
        //[TestCaseSource("PasswordPageConfig")]
        //public async Task PasswordPageTest(Config config)
        //{
        //    Crawler crawler = new Crawler(config);
        //    List<IEntry> entries = new List<IEntry>();

        //    await foreach (var entry in crawler.Process())
        //    {
        //        entries.Add(entry);
        //    }

        //    Assert.IsInstanceOf(typeof(PasswordProtectedEntry), entries.First());
        //}

        //public static IEnumerable<Config> FailedPageConfig
        //{
        //    get
        //    {
        //        yield return new Config()
        //        {
        //            StartPageId = 999999999,
        //            EndPageId = 999999999,
        //            MaxTriesPerPage = 10
        //        };
        //    }
        //}

        //[Test]
        //[TestCaseSource("FailedPageConfig")]
        //public async Task FailedPageTest(Config config)
        //{
        //    Crawler crawler = new Crawler(config);
        //    List<IEntry> entries = new List<IEntry>();

        //    await foreach (var entry in crawler.Process())
        //    {
        //        entries.Add(entry);
        //    }

        //    Assert.IsInstanceOf(typeof(FailedEntry), entries.First());
        //}

        //public static IEnumerable<Config> AuthorAndDateConfig
        //{
        //    get
        //    {
        //        yield return new Config()
        //        {
        //            StartPageId = 50,
        //            EndPageId = 52,
        //            MaxTriesPerPage = 10
        //        };
        //    }
        //}

        //[Test]
        //[TestCaseSource("AuthorAndDateConfig")]
        //public async Task AuthorAndDateTest(Config config)
        //{
        //    Crawler crawler = new Crawler(config);
        //    List<IEntry> entries = new List<IEntry>();

        //    await foreach (var entry in crawler.Process())
        //    {
        //        entries.Add(entry);
        //    }

        //    var id50 = (TextEntry)entries.First(entry => entry.ID == 50);
        //    Assert.AreEqual("\\\\", id50.Author);
        //    Assert.AreEqual("2007-05-08 09:31", id50.Date.ToString("yyyy-MM-dd HH:mm"));

        //    var id51 = (TextEntry)entries.First(entry => entry.ID == 51);
        //    Assert.AreEqual("Borys", id51.Author);
        //    Assert.AreEqual("2007-05-08 09:56", id51.Date.ToString("yyyy-MM-dd HH:mm"));

        //    var id52 = (TextEntry)entries.First(entry => entry.ID == 52);
        //    Assert.AreEqual(@"jamal", id52.Author);
        //    Assert.AreEqual("2007-05-08 11:18", id52.Date.ToString("yyyy-MM-dd HH:mm"));
        //}

        //public static IEnumerable<Config> OCRTestConfig
        //{
        //    get
        //    {
        //        yield return new Config()
        //        {
        //            StartPageId = 4,
        //            EndPageId = 4,
        //            MaxTriesPerPage = 10,
        //            OCRApiKey = "34855a945e88957"
        //        };
        //    }
        //}

        //[Test]
        //[TestCaseSource("OCRTestConfig")]
        //public async Task OCRTest(Config config)
        //{
        //    Crawler crawler = new Crawler(config);
        //    List<IEntry> entries = new List<IEntry>();

        //    await foreach (var entry in crawler.Process())
        //    {
        //        entries.Add(entry);
        //    }

        //    var picture = (PictureEntry)entries.First();
        //    StringAssert.Contains("Image dimensions are too large!", picture.OCRResponse);
        //}
    }
}