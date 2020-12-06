using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper.Interfaces;

namespace Core.Models.DataParser.Abstract
{
    internal abstract class DataParserHandler : IDataParserHandler
    {
        private IDataParserHandler _nextHandler;

        public IDataParserHandler SetNext(IDataParserHandler handler)
        {
            _nextHandler = handler;
            
            return handler;
        }
        
        public virtual IEntry Handle(IScrapedElement scrapedElement, int id)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(scrapedElement, id);
            }
            else
            {
                return new FailedEntry()
                {
                    ID = id,
                    StatusCode = -1
                };
            }
        }
    }
}
