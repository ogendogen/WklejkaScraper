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
    internal class PictureEntryHandler : DataParserHandler
    {
        public override IEntry Handle(IScrapedElement scrapedElement)
        {
            if (scrapedElement is ScrapedPictureElement pictureElement)
            {
                return new PictureEntry()
                {
                    PicturePath = pictureElement.Path
                    // todo: OCR here
                };
            }

            return base.Handle(scrapedElement);
        }
    }
}
