using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using USSEScoreboard.Data;
using USSEScoreboard.Models;

namespace USSEScoreboard.Controllers
{
    public class ScoreboardItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreboardItemsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ScoreboardItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ScoreboardItem.Include(s => s.UserProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ScoreboardItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreboardItem = await _context.ScoreboardItem.SingleOrDefaultAsync(m => m.ScoreboardItemId == id);
            if (scoreboardItem == null)
            {
                return NotFound();
            }

            return View(scoreboardItem);
        }

        // GET: ScoreboardItems/Create
        public IActionResult Create()
        {
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FullName");
            ViewData["DateCreated"] = DateTime.Now;
            ViewData["DateModified"] = DateTime.Now;
            return View();
        }

        // POST: ScoreboardItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScoreboardItemId,DateCreated,DateModified,Description,Title,Total,UserProfileId")] ScoreboardItem scoreboardItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scoreboardItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", scoreboardItem.UserProfileId);
            return View(scoreboardItem);
        }

        // GET: ScoreboardItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreboardItem = await _context.ScoreboardItem.SingleOrDefaultAsync(m => m.ScoreboardItemId == id);
            if (scoreboardItem == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FullName", scoreboardItem.UserProfileId);
            return View(scoreboardItem);
        }

        // POST: ScoreboardItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreboardItemId,DateCreated,DateModified,Description,Title,Total,UserProfileId")] ScoreboardItem scoreboardItem)
        {
            if (id != scoreboardItem.ScoreboardItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    scoreboardItem.DateModified = DateTime.Now;
                    _context.Update(scoreboardItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreboardItemExists(scoreboardItem.ScoreboardItemId))
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
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", scoreboardItem.UserProfileId);
            return View(scoreboardItem);
        }

        // GET: ScoreboardItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreboardItem = await _context.ScoreboardItem.SingleOrDefaultAsync(m => m.ScoreboardItemId == id);
            if (scoreboardItem == null)
            {
                return NotFound();
            }

            return View(scoreboardItem);
        }

        // POST: ScoreboardItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scoreboardItem = await _context.ScoreboardItem.SingleOrDefaultAsync(m => m.ScoreboardItemId == id);
            _context.ScoreboardItem.Remove(scoreboardItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ScoreboardItemExists(int id)
        {
            return _context.ScoreboardItem.Any(e => e.ScoreboardItemId == id);
        }
    }
}
