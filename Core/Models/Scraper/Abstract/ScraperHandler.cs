using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Interfaces;
using HtmlAgilityPack;

namespace Core.Models.Scraper.Abstract
{
    internal abstract class ScraperHandler : IScraperHandler
    {
        private IScraperHandler _nextHandler;

        public IScraperHandler SetNext(IScraperHandler handler)
        {
            _nextHandler = handler;
            
            return handler;
        }
        
        public virtual IScrapedElement Handle(HtmlDocument htmlDoc, string name)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(htmlDoc, name);
            }
            else
            {
                return null;
            }
        }
    }
}
