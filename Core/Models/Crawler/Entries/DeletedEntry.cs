﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.Crawler.Interfaces;

namespace Core.Models.Crawler.Entries
{
    public class DeletedEntry : IEntry
    {
        public int ID { get; set; }
    }
}