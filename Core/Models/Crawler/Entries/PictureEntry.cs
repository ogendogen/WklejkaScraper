using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Crawler.Interfaces;

namespace Core.Models.Crawler.Entries
{
    public class PictureEntry : IEntry
    {
        public int ID { get; set; }
        public string PicturePath { get; set; }
        public byte[] Picture { get; set; }
        public string ReadPicture { get; set; }
    }
}
