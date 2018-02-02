﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboard.Website.Models
{
    public class HighlightSearchResult
    {
        public int HighlightId { get; set; }
        public string FullName { get; set; }
        public string Body { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
