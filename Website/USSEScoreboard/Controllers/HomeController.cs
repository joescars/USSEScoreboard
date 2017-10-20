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
        private readonly IDashboardService _dashboardService;

        public HomeController(
            ApplicationDbContext context,             
            IDashboardService dashboardService)
        {
            _context = context;
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            
            var model = new DashboardViewModel();

            //Team Profile Data
            model.TeamTotals = _dashboardService.GetTeamScores();
            
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
