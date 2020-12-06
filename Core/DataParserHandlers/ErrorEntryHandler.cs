using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.DataParser.Abstract;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;

namespace Core.DataParserHandlers
{
    internal class ErrorEntryHandler : DataParserHandler
    {
        public override IEntry Handle(IScrapedElement scrapedElement)
        {
            if (scrapedElement is ScrapedTextElement textElement && textElement.Content == "error")
            {
                return new FailedEntry()
                {
                    
                };
            }

            return base.Handle(scrapedElement);
        }
    }
}
