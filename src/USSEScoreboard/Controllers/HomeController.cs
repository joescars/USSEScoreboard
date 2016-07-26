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
            //var myTeam = _context.UserProfile.OrderBy(u => u.FirstName).ToList();

            var myTeam = (from u in _context.UserProfile
                          orderby u.FullName ascending
                          select new UserScore
                          {
                              UserProfileId = u.UserProfileId,
                              FullName = u.FullName,
                              IsCRM = u.IsCRM,
                              IsExpenses = u.IsExpenses,
                              TotalPresentations = u.TotalPresentations,
                              TotalAscend = u.TotalAscend,
                              CommitTotal = u.Commitments
                              .Where(c => c.Status == CommitmentStatus.InProgress 
                              || c.Status == CommitmentStatus.Completed).Count(),
                              CommitCompleted = u.Commitments
                             .Where(c => c.Status == CommitmentStatus.Completed).Count(),
                         });

            ViewData["Team"] = myTeam;

            //Totals
            var TotalPresentations = _context.UserProfile.Sum(u => u.TotalPresentations);
            ViewData["TotalPresentations"] = TotalPresentations;

            var TotalAscend = _context.UserProfile.Sum(u => u.TotalAscend);
            ViewData["TotalAscend"] = TotalAscend;
           
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
