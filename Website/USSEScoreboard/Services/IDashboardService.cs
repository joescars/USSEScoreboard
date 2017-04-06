using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Models;

namespace USSEScoreboard.Services
{
    public interface IDashboardService
    {
        IQueryable<UserScore> GetTeamScores();
        int GetTotalPresentations();
        int GetTotalAscendActive();
        int GetTotalAscendComplete();
        int GetTotalAscendProposed();
        int GetTotalAscendWins();
    }
}
