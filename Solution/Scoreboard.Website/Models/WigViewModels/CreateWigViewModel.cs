﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboard.Website.Models.WigViewModels
{
    public class CreateWigViewModel
    {
        public int WigId { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public WigStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public IList<UserProfile> UserProfiles { get; set; }
        public int[] SelectedUserProfiles { get; set; }
        public CreateWigViewModel()
        {
            DateCreated = DateTime.Today;
        }
    }
}
