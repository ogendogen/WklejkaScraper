using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Scraper.Interfaces;

namespace Core.Models.DataParser.Interfaces
{
    interface IDataParserHandler
    {
        IDataParserHandler SetNext(IDataParserHandler handler);
        IEntry Handle(IScrapedElement scrapedElement, int id);
    }
}
