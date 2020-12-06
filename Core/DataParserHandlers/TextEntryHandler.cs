using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.DataParser.Abstract;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper.Interfaces;

namespace Core.DataParserHandlers
{
    internal class TextEntryHandler : DataParserHandler
    {
        public override IEntry Handle(IScrapedElement scrapedElement, int id)
        {
            throw new NotImplementedException();
        }
    }
}
