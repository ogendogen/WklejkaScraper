using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models.DataParser.Abstract;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;

namespace Core.DataParserHandlers
{
    internal class PictureEntryHandler : DataParserHandler
    {
        public RequestHandler RequestHandler { get; set; }
        public PictureEntryHandler(int maxTries)
        {
            RequestHandler = new RequestHandler(maxTries);
        }

        public override IEntry Handle(IScrapedElement scrapedElement, int id)
        {
            if (scrapedElement is ScrapedPictureElement pictureElement)
            {
                byte[] pictureBytes = DownloadPicture(pictureElement.Path);
                return new PictureEntry()
                {
                    ID = id,
                    PicturePath = pictureElement.Path,
                    Picture = pictureBytes,
                    ReadPicture = OCR.ProcessImage(pictureBytes).Result
                };
            }

            return base.Handle(scrapedElement, id);
        }

        private byte[] DownloadPicture(string path)
        {
            return RequestHandler.GetImage(path);
        }
    }
}
