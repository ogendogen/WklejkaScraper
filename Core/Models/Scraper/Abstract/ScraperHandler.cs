using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Interfaces;

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
        
        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
