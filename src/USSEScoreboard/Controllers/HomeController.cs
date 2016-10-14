using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USSEScoreboard.Data;
using Microsoft.AspNetCore.Identity;
using USSEScoreboard.Models;

namespace USSEScoreboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //Team Profile Data
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

            ViewData["Team"] = myTeam;

            /*
            // Totals by User
            var TotalPresentations = _context.UserProfile.Sum(u => u.TotalPresentations);
            ViewData["TotalPresentations"] = TotalPresentations;

            var TotalAscend = _context.UserProfile.Sum(u => u.TotalAscend);
            ViewData["TotalAscend"] = TotalAscend;
            */

            // Totals Globally
            var TotalPresentations = _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.Presentations)
                .Sum(a => a.TimeFrameTotal);
            ViewData["TotalPresentations"] = TotalPresentations;

            var TotalAscendActive = _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendActive)
                .Sum(a => a.TimeFrameTotal);
            ViewData["TotalAscendActive"] = TotalAscendActive;

            var TotalAscendComplete = _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendCodeCompleted)
                .Sum(a => a.TimeFrameTotal);
            ViewData["TotalAscendComplete"] = TotalAscendComplete;

            var TotalAscendProposed = _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendProposed)
                .Sum(a => a.TimeFrameTotal);
            ViewData["TotalAscendProposed"] = TotalAscendProposed;

            var TotalAscendWins = _context.GlobalScoreEntry
                .Where(a => a.GlobalScoreType == GlobalScoreEntryType.AscendWins)
                .Sum(a => a.TimeFrameTotal);
            ViewData["TotalAscendWins"] = TotalAscendWins;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
