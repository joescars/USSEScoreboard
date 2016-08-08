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

namespace USSEScoreboard.Controllers
{
    [Authorize]
    public class ScoreEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreEntriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ScoreEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ScoreEntry.Include(s => s.UserProfile)
                .OrderByDescending(s => s.DateCreated);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ScoreEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreEntry = await _context.ScoreEntry.SingleOrDefaultAsync(m => m.ScoreEntryId == id);
            if (scoreEntry == null)
            {
                return NotFound();
            }

            return View(scoreEntry);
        }

        // GET: ScoreEntries/Create
        public IActionResult Create()
        {
                
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FullName");
            return View();
        }

        // POST: ScoreEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScoreEntryId,DateCreated,DateModified,ScoreType,Total,UserProfileId,WeekEnding")] ScoreEntry scoreEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scoreEntry);

                //Update the total Score
                var up = await _context.UserProfile.Where(u => u.UserProfileId == scoreEntry.UserProfileId).FirstOrDefaultAsync();

                // TODO: Create function for this
                // Update column based on presentation type
                switch (scoreEntry.ScoreType)
                {
                    case ScoreEntryType.Presentation:
                        up.TotalPresentations += scoreEntry.Total;
                        break;
                    case ScoreEntryType.Ascend:
                        up.TotalAscend += scoreEntry.Total;
                        break;
                    default:
                        break;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", scoreEntry.UserProfileId);
            return View(scoreEntry);
        }

        // GET: ScoreEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreEntry = await _context.ScoreEntry.SingleOrDefaultAsync(m => m.ScoreEntryId == id);
            if (scoreEntry == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", scoreEntry.UserProfileId);
            return View(scoreEntry);
        }

        // POST: ScoreEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreEntryId,DateCreated,DateModified,ScoreType,Total,UserProfileId,WeekEnding")] ScoreEntry scoreEntry)
        {
            if (id != scoreEntry.ScoreEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scoreEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreEntryExists(scoreEntry.ScoreEntryId))
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
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", scoreEntry.UserProfileId);
            return View(scoreEntry);
        }

        // GET: ScoreEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreEntry = await _context.ScoreEntry.SingleOrDefaultAsync(m => m.ScoreEntryId == id);
            if (scoreEntry == null)
            {
                return NotFound();
            }

            return View(scoreEntry);
        }

        // POST: ScoreEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scoreEntry = await _context.ScoreEntry.SingleOrDefaultAsync(m => m.ScoreEntryId == id);
            _context.ScoreEntry.Remove(scoreEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: ScoreEntries/ResetCRMExpenses
        [HttpGet]
        public async Task<IActionResult> ResetCRMExpenses()
        {
            var up = await _context.UserProfile.ToListAsync();
            foreach (UserProfile u in up)
            {
                u.IsCRM = false;
                u.IsExpenses = false;   
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { msg = "reset" });
        }

        // GET: ScoreEntries/ArchiveCommits
        [HttpGet]
        public async Task<IActionResult> ArchiveCommits()
        {
            var commits = await _context.Commitment
                .Where(c => c.Status == CommitmentStatus.Complete)
                .ToListAsync();
            foreach (Commitment c in commits)
            {
                c.Status = CommitmentStatus.Archive;
                c.DateModified = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { msg = "archived"});
        }

        // GET: ScoreEntries/ResetFRI
        [HttpGet]
        public async Task<IActionResult> ResetFRI()
        {
            var up = await _context.UserProfile.ToListAsync();
            foreach (UserProfile u in up)
            {
                u.IsFRI = false;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { msg = "resetfri" });
        }

        private bool ScoreEntryExists(int id)
        {
            return _context.ScoreEntry.Any(e => e.ScoreEntryId == id);
        }
    }
}
