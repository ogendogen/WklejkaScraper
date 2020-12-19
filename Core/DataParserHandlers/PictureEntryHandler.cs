using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.DataParser.Abstract;
using Core.Models.DataParser.Entries;
using Core.Models.DataParser.Interfaces;
using Core.Models.Scraper.Elements;
using Core.Models.Scraper.Interfaces;
using Newtonsoft.Json.Linq;

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
                string ocrRawResponse = OCR.ProcessImage(pictureElement.Path).Result;
                string parsedOCRResponse = ParseOCRResponse(ocrRawResponse);      

                return new PictureEntry()
                {
                    ID = id,
                    PicturePath = pictureElement.Path,
                    Picture = pictureBytes,
                    OCRResponse = parsedOCRResponse
                };
            }

            return base.Handle(scrapedElement, id);
        }

        private string ParseOCRResponse(string ocrRawResponse)
        {
            JObject ocrJsonObject = JObject.Parse(ocrRawResponse);
            
            string parsedText = ocrJsonObject["ParsedResults"]?[0]?["ParsedText"]?.ToString();
            string errorMessage = ocrJsonObject["ParsedResults"]?[0]?["ErrorMessage"]?.ToString();
            string errorDetails = ocrJsonObject["ParsedResults"]?[0]?["ErrorDetails"]?.ToString();

            if (!String.IsNullOrEmpty(parsedText))
            {
                return parsedText;
            }
            else if (!String.IsNullOrEmpty(errorMessage) || !String.IsNullOrEmpty(errorDetails))
            {
                return errorMessage + " " + errorDetails;
            }

            return ocrRawResponse;
        }

        private byte[] DownloadPicture(string path)
        {
            return RequestHandler.GetImage(path);
        }
    }
}
