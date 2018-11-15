using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Scoreboard.Website.Models;
using Scoreboard.Website.Services;
using Scoreboard.Website.Interfaces;
using Scoreboard.Website.Models.HomeViewModels;
using Scoreboard.Common.Entities;
using Scoreboard.Common.Data;

namespace Scoreboard.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDashboardService _dashboardService;
        private readonly IToggleService _toggleService;

        public HomeController(
            ApplicationDbContext context,             
            IDashboardService dashboardService,
            IToggleService toggleService)
        {
            _context = context;
            _dashboardService = dashboardService;
            _toggleService = toggleService;
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

        // GET: Commitments/ToggleExpensesUser/1
        [HttpGet]
        public async Task<IActionResult> ToggleExpensesUser(int id)
        {
            await _toggleService.ToggleUserExpense(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Commitments/ToggleCRMUser/1 (userprofiled)
        [HttpGet]
        public async Task<IActionResult> ToggleCRMUser(int id)
        {
            await _toggleService.ToggleUserCRM(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Commitments/ToggleFRIUser/1
        [HttpGet]
        public async Task<IActionResult> ToggleFRIUser(int id)
        {
            await _toggleService.ToggleUserFRI(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Commitments/ToggleAscendNotes/User/1
        [HttpGet]
        public async Task<IActionResult> ToggleAscendNotesUser(int id)
        {
            await _toggleService.ToggleUserAscendNotes(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
