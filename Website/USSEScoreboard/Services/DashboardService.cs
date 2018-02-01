using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Models;
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserScore> GetTeamScores()
        {
            var myTeam = (from u in _context.UserProfile
                          where u.IsActiveTeamMember == true
                          orderby u.FullName ascending
                          select new UserScore
                          {
                              UserProfileId = u.UserProfileId,
                              FullName = u.FullName,
                              IsCRM = u.IsCRM,
                              IsExpenses = u.IsExpenses,
                              IsFRI = u.IsFRI,
                              IsAscendNotes = u.IsAscendNotes,
                              TotalPresentations = u.TotalPresentations,
                              TotalAscend = u.TotalAscend,
                              CommitTotal = 0,
                              CommitCompleted = 0,
                          });

            return myTeam;
        }

        public int GetTotalAscendActive()
        {
            return 0;
        }

        public int GetTotalAscendComplete()
        {
            return 0;
        }

        public int GetTotalAscendProposed()
        {
            return 0;
        }

        public int GetTotalAscendWins()
        {
            return 0;
        }

        public int GetTotalPresentations()
        {
            return 0;
        }
    }
}
