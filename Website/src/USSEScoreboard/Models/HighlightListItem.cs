﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class HighlightListItem
    {
        public int HighlightId { get; set; }
        public string FullName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
