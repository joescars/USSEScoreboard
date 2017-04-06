using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USSEScoreboard.Data;
using Microsoft.AspNetCore.Identity;
using USSEScoreboard.Models;
using USSEScoreboard.Services;
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWIGSettingRepository _wigSettingRepository;
        private readonly IDashboardService _dashboardService;

        public HomeController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            IWIGSettingRepository wigSettingRepository,
            IDashboardService dashboardService)
        {
            _context = context;
            _userManager = userManager;
            _wigSettingRepository = wigSettingRepository;
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            //Team Profile Data
            ViewData["Team"] = _dashboardService.GetTeamScores();

            // Totals Globally
            var TotalPresentations = _dashboardService.GetTotalPresentations();
            ViewData["TotalPresentations"] = TotalPresentations;

            var TotalAscendActive = _dashboardService.GetTotalAscendActive();
            ViewData["TotalAscendActive"] = TotalAscendActive;

            var TotalAscendComplete = _dashboardService.GetTotalAscendComplete();
            ViewData["TotalAscendComplete"] = TotalAscendComplete;

            var TotalAscendProposed = _dashboardService.GetTotalAscendProposed();
            ViewData["TotalAscendProposed"] = TotalAscendProposed;

            var TotalAscendWins = _dashboardService.GetTotalAscendWins();
            ViewData["TotalAscendWins"] = TotalAscendWins;

            // Grab the settings from the repo
            WIGSetting wg = _wigSettingRepository.GetSettings();
            ProgressCalculator pg = new ProgressCalculator();
            ProgressCalculator.Result result = pg.CalculateProgress(
                wg.StartDate, wg.EndDate, wg.AscendWinGoal, 
                wg.CommunityWinGoal, TotalAscendWins, TotalPresentations);

            ViewData["AscendProgress"] = result.AscendProgressPct;
            ViewData["AscendOverall"] = result.AscendOverallPct;
            ViewData["CommunityProgress"] = result.CommunityProgressPct;
            ViewData["CommunityOverall"] = result.CommunityOverallPct;

            ViewData["AscendProgressClass"] = GetProgressBarClass(result.AscendProgressPct);
            ViewData["AscendOverallClass"] = GetProgressBarClass(result.AscendOverallPct);
            ViewData["CommunityProgressClass"] = GetProgressBarClass(result.CommunityProgressPct);
            ViewData["CommunityOverallClass"] = GetProgressBarClass(result.CommunityOverallPct);

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

        private string GetProgressBarClass(int Value)
        {
            //progress-bar-info
            //progress-bar-success
            //progress-bar-warning

            if (Value > 90)
            {
                return "progress-bar-success";
            }
            else if (Value > 60)
            {
                return "progress-bar-info";
            }
            else
            {
                return "progress-bar-warning";
            }

        }
    }
}
