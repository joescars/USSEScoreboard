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
using USSEScoreboard.Models.HomeViewModels;

namespace USSEScoreboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWIGSettingRepository _wigSettingRepository;
        private readonly IDashboardService _dashboardService;

        public HomeController(
            ApplicationDbContext context,             
            IWIGSettingRepository wigSettingRepository,
            IDashboardService dashboardService)
        {
            _context = context;
            //_userManager = userManager;
            _wigSettingRepository = wigSettingRepository;
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            
            var model = new DashboardViewModel();
            

            //Team Profile Data
            model.TeamTotals = _dashboardService.GetTeamScores();

            // Totals Globally
            model.TotalPresentations = _dashboardService.GetTotalPresentations();
            model.TotalAscendActive = _dashboardService.GetTotalAscendActive();
            model.TotalAscendComplete = _dashboardService.GetTotalAscendComplete();
            model.TotalAscendProposed = _dashboardService.GetTotalAscendProposed();
            model.TotalAscendWins = _dashboardService.GetTotalAscendWins();

            // Grab the settings from the repo
            WIGSetting wg = _wigSettingRepository.GetSettings();
            ProgressCalculator pg = new ProgressCalculator();
            ProgressCalculator.Result result = pg.CalculateProgress(
                wg.StartDate, wg.EndDate, wg.AscendWinGoal,
                wg.CommunityWinGoal, model.TotalAscendWins, model.TotalPresentations);

            // Overalls
            model.AscendProgress = result.AscendProgressPct;
            model.AscendOverall = result.AscendOverallPct;
            model.CommunityProgress = result.CommunityProgressPct;
            model.CommunityOverall = result.CommunityOverallPct;

            // Assigned classes
            model.AscendProgressClass = GetProgressBarClass(result.AscendProgressPct);
            model.AscendOverallClass = GetProgressBarClass(result.AscendOverallPct);
            model.CommunityProgressClass = GetProgressBarClass(result.CommunityProgressPct);
            model.CommunityOverallClass = GetProgressBarClass(result.CommunityOverallPct);

            return View(model);
            
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

        public string GetProgressBarClass(int Value)
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
