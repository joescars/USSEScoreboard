using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboard.Website.Models.HomeViewModels
{
    public class DashboardViewModel
    {
        public IQueryable<UserScore> TeamTotals { get; set; }
        public int TotalPresentations { get; set; }
        public int TotalAscendActive { get; set; }
        public int TotalAscendComplete { get; set; }
        public int TotalAscendProposed { get; set; }
        public int TotalAscendWins { get; set; }
        public int AscendProgress { get; set; }
        public int AscendOverall { get; set; }
        public int CommunityProgress { get; set; }
        public int CommunityOverall { get; set; }
        public string AscendProgressClass { get; set; }
        public string AscendOverallClass { get; set; }
        public string CommunityProgressClass { get; set; }
        public string CommunityOverallClass { get; set; }
    }
}
