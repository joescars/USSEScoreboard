using Microsoft.AspNetCore.Mvc.Rendering;
using Scoreboard.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboard.Website.Models.HighlightViewModels
{
    public class ListHighlightsViewModel
    {
        public IEnumerable<HighlightListItem> Highlights { get; set; }
        public IEnumerable<UserProfile> ActiveUsers { get; set; }
        public SelectList ActiverUserList { get; set; }
        public int SearchByUserProfileId { get; set; }
    }
}
