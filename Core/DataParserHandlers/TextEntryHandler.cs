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
    internal class TextEntryHandler : DataParserHandler
    {
        public override IEntry Handle(IScrapedElement scrapedElement, int id)
        {
            if (scrapedElement is ScrapedTextElement textElement && !String.IsNullOrEmpty(textElement.Content))
            {
                return new TextEntry()
                {
                    ID = id,
                    Content = textElement.Content
                };
            }

            return base.Handle(scrapedElement, id);
        }
    }
}
