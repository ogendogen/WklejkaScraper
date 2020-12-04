using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Models.Scraper;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using Core.ScraperHandlers;
using HtmlAgilityPack;

namespace Core
{
    internal class Scraper
    {
        internal Scraper()
        {

        }

        internal IEnumerable<IScrapedElement> ScrapAllFromContent(string content)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);

            yield return GetContent(htmlDoc);
            yield return GetAuthor(htmlDoc);
            yield return GetDate(htmlDoc);
        }
        
        private IScrapedElement GetContent(HtmlDocument htmlDoc)
        {
            var textHandler = new TextHandler();
            var pictureHandler = new PictureHandler();
            var passwordHandler = new PasswordHandler();
            var deletedHandler = new DeletedHandler();

            textHandler.SetNext(pictureHandler)
                .SetNext(passwordHandler)
                .SetNext(deletedHandler);

            return textHandler.Handle(htmlDoc);
        }

        private IScrapedElement GetAuthor(HtmlDocument htmlDoc)
        {
            var authorHandler = new AuthorHandler();

            return authorHandler.Handle(htmlDoc);
        }

        private IScrapedElement GetDate(HtmlDocument htmlDoc)
        {
            var dateHandler = new DateHandler();

            return dateHandler.Handle(htmlDoc);
        }
    }
}
