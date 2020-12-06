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
    internal class DeletedEntryHandler : DataParserHandler
    {
        public override IEntry Handle(IScrapedElement scrapedElement)
        {
            if (scrapedElement is ScrapedTextElement textElement && textElement.Content == "deleted")
            {
                return new DeletedEntry()
                {
                    
                };
            }

            return base.Handle(scrapedElement);
        }
    }
}
