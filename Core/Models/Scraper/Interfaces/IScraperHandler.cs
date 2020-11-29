using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Scraper.Interfaces
{
    interface IScraperHandler
    {
        IScraperHandler SetNext(IScraperHandler handler);
        object Handle(object request);
    }
}
