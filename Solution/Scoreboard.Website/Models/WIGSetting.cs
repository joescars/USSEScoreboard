using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class WIGSetting
    {
        public int WIGSettingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AscendWinGoal { get; set; }
        public double CommunityWinGoal { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
