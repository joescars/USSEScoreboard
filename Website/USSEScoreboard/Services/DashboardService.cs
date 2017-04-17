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
                              CommitTotal = u.Commitments
                              .Where(c => c.Status == CommitmentStatus.Active
                              || c.Status == CommitmentStatus.Complete).Count(),
                              CommitCompleted = u.Commitments
                             .Where(c => c.Status == CommitmentStatus.Complete).Count(),
                          });

            return myTeam;
        }

        public int GetTotalAscendActive()
        {
            return _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendActive)
                .Sum(a => a.TimeFrameTotal);
        }

        public int GetTotalAscendComplete()
        {
            return _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendCodeCompleted)
                .Sum(a => a.TimeFrameTotal);
        }

        public int GetTotalAscendProposed()
        {
            return _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendProposed)
                .Sum(a => a.TimeFrameTotal);
        }

        public int GetTotalAscendWins()
        {
            return _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendWins)
                .Sum(a => a.TimeFrameTotal);
        }

        public int GetTotalPresentations()
        {
            return _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.Presentations)
                .Sum(a => a.TimeFrameTotal);
        }
    }
}
