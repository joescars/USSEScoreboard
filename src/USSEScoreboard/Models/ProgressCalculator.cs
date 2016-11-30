using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Models;
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Models
{
    public class ProgressCalculator
    {
        public class Result
        {
            public int AscendProgressPct { get; set; }
            public int AscendOverallPct { get; set; }
            public int CommunityProgressPct { get; set; }
            public int CommunityOverallPct { get; set; }
        }

        public Result CalculateProgress(
            DateTime StartDate,
            DateTime EndDate,
            double AscendWinGoal,
            double CommunityWinGoal,
            double CurrentAscend,
            double CurrentCommunity)
        {
            Result myResult = new Result();

            // Date Stuff
            TimeSpan tsTotal = EndDate - StartDate;
            TimeSpan tsCurrent = DateTime.Today - StartDate;
            double daysTotal = tsTotal.Days;
            double daysCurrent = tsCurrent.Days;
            double yearProgress = (daysCurrent / daysTotal);

            // Goal Stuff            
            double CommunityCurrentGoal = yearProgress * CommunityWinGoal;
            double CommunityCurrentProgress = (CurrentCommunity / CommunityCurrentGoal) * 100;
            double CommunityOverallProgress = (CurrentCommunity / CommunityWinGoal) * 100;

            double AscendCurrentGoal = yearProgress * AscendWinGoal;
            double AscendCurrentProgress = (CurrentAscend / AscendCurrentGoal) * 100;
            double AscendOverallProgress = (CurrentAscend / AscendWinGoal) * 100;

            // Wire up the results and send it back
            myResult.CommunityProgressPct = Convert.ToInt32(Math.Round(CommunityCurrentProgress));
            myResult.CommunityOverallPct = Convert.ToInt32(Math.Round(CommunityOverallProgress));
            myResult.AscendProgressPct = Convert.ToInt32(Math.Round(AscendCurrentProgress));
            myResult.AscendOverallPct = Convert.ToInt32(Math.Round(AscendOverallProgress));

            // Return the result
            return myResult;

        }
    }
}
