using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using USSEScoreboard.Data;
using USSEScoreboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Controllers
{
    [Authorize]
    public class HighlightsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHighlightRepository _highlightRepository;

        public HighlightsController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            IHighlightRepository highlightrepository)
        {
            _context = context;
            _userManager = userManager;
            _highlightRepository = highlightrepository;
        }

        // GET: Highlights
        public async Task<IActionResult> Index()
        {
            return View(await _highlightRepository.GetHighlightsAsync());
        }

        // GET: Highlights/My
        public async Task<IActionResult> My()
        {
            var userId = _userManager.GetUserId(User);            
            return View(await _highlightRepository.GetHighlightsByUserId(userId));
        }

        // GET: Highlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }

            return View(highlight);
        }

        // GET: Highlights/Create
        public IActionResult Create()
        {
            var model = new Highlight();
            var WeekStart = _highlightRepository.GetStartOfWeek();
            model.DateStart = WeekStart;
            model.DateEnd = WeekStart.AddDays(4);
            return View(model);
        }

        // POST: Highlights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HighlightId,Body,DateEnd,DateStart,UserProfileId")] Highlight highlight)
        {
            if (ModelState.IsValid)
            {
                // Get User Profile
                var userId = _userManager.GetUserId(User);
                var userProfileId = await _context.UserProfile
                    .Where(u => u.UserId == userId)
                    .Select(u => u.UserProfileId).FirstOrDefaultAsync();
                                
                await _highlightRepository.SaveHighlightAsync(highlight, userProfileId);
                return RedirectToAction("Index");
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FullName", highlight.UserProfileId);
            return View(highlight);
        }

        // GET: Highlights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FullName", highlight.UserProfileId);
            return View(highlight);
        }

        // POST: Highlights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HighlightId,Body,DateCreated,DateEnd,DateModified,DateStart,UserProfileId")] Highlight highlight)
        {
            if (id != highlight.HighlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _highlightRepository.UpdateHighlightAsync(highlight);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_highlightRepository.HighlightExists(highlight.HighlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FullName", highlight.UserProfileId);
            return View(highlight);
        }

        // GET: Highlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }

            return View(highlight);
        }

        // POST: Highlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            await _highlightRepository.DeleteHighlightAsync(highlight);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(DateTime startDate, DateTime endDate)
        {
            return View(await _highlightRepository.GetHighlightsByDateRange(startDate, endDate));
        }

    }
}
