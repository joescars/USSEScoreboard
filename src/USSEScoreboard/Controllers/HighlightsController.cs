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

namespace USSEScoreboard.Controllers
{
    [Authorize]
    public class HighlightsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HighlightsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Highlights
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Highlight.Include(h => h.UserProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Highlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _context.Highlight.SingleOrDefaultAsync(m => m.HighlightId == id);
            if (highlight == null)
            {
                return NotFound();
            }

            return View(highlight);
        }

        // GET: Highlights/Create
        public IActionResult Create()
        {
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId");
            return View();
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

                highlight.UserProfileId = userProfileId;
                highlight.DateCreated = DateTime.Now;
                highlight.DateModified = DateTime.Now;

                _context.Add(highlight);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", highlight.UserProfileId);
            return View(highlight);
        }

        // GET: Highlights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _context.Highlight.SingleOrDefaultAsync(m => m.HighlightId == id);
            if (highlight == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", highlight.UserProfileId);
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
                    _context.Update(highlight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HighlightExists(highlight.HighlightId))
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
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", highlight.UserProfileId);
            return View(highlight);
        }

        // GET: Highlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _context.Highlight.SingleOrDefaultAsync(m => m.HighlightId == id);
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
            var highlight = await _context.Highlight.SingleOrDefaultAsync(m => m.HighlightId == id);
            _context.Highlight.Remove(highlight);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HighlightExists(int id)
        {
            return _context.Highlight.Any(e => e.HighlightId == id);
        }
    }
}
