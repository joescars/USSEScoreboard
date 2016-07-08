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
    public class ScoreboardEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreboardEntriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ScoreboardEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ScoreboardEntry
                .Include(s => s.ScoreboardItem)
                .Include(u => u.ScoreboardItem.UserProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ScoreboardEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreboardEntry = await _context.ScoreboardEntry.SingleOrDefaultAsync(m => m.ScoreboardEntryId == id);
            if (scoreboardEntry == null)
            {
                return NotFound();
            }

            return View(scoreboardEntry);
        }

        // GET: ScoreboardEntries/Create
        public IActionResult Create()
        {

            ViewData["ScoreboardItemId"] = GetScoreboardItems();
            ViewData["DateCreated"] = DateTime.Now;
            ViewData["DateModified"] = DateTime.Now;

            return View();
        }

        // POST: ScoreboardEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScoreboardEntryId,Count,DateCreated,DateModified,ScoreboardItemId")] ScoreboardEntry scoreboardEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scoreboardEntry);
                await _context.SaveChangesAsync();

                //Update Total for the parent item
                var myScoreboardItem = 
                    await _context.ScoreboardItem.Where(i => i.ScoreboardItemId == scoreboardEntry.ScoreboardItemId).FirstOrDefaultAsync();
                myScoreboardItem.Total += scoreboardEntry.Count;
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            ViewData["ScoreboardItemId"] = new SelectList(GetScoreboardItems(), "Value", "Text", scoreboardEntry.ScoreboardItemId);
            return View(scoreboardEntry);
        }

        // GET: ScoreboardEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreboardEntry = await _context.ScoreboardEntry.SingleOrDefaultAsync(m => m.ScoreboardEntryId == id);
            if (scoreboardEntry == null)
            {
                return NotFound();
            }
            ViewData["ScoreboardItemId"] = new SelectList(GetScoreboardItems(),"Value","Text",scoreboardEntry.ScoreboardItemId);
            return View(scoreboardEntry);
        }

        // POST: ScoreboardEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreboardEntryId,Count,DateCreated,DateModified,ScoreboardItemId")] ScoreboardEntry scoreboardEntry)
        {
            if (id != scoreboardEntry.ScoreboardEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Todo: Add edit feature to change score
                    _context.Update(scoreboardEntry);                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreboardEntryExists(scoreboardEntry.ScoreboardEntryId))
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
            ViewData["ScoreboardItemId"] = new SelectList(GetScoreboardItems(), "Value", "Text", scoreboardEntry.ScoreboardItemId);
            return View(scoreboardEntry);
        }

        // GET: ScoreboardEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreboardEntry = await _context.ScoreboardEntry.SingleOrDefaultAsync(m => m.ScoreboardEntryId == id);
            if (scoreboardEntry == null)
            {
                return NotFound();
            }

            return View(scoreboardEntry);
        }

        // POST: ScoreboardEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scoreboardEntry = await _context.ScoreboardEntry
                .Include(s => s.ScoreboardItem)
                .SingleOrDefaultAsync(m => m.ScoreboardEntryId == id);

            //remove this amount from the total score
            scoreboardEntry.ScoreboardItem.Total -= scoreboardEntry.Count;

            _context.ScoreboardEntry.Remove(scoreboardEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ScoreboardEntryExists(int id)
        {
            return _context.ScoreboardEntry.Any(e => e.ScoreboardEntryId == id);
        }

        private List<SelectListItem> GetScoreboardItems()
        {
            var mySelects = _context.ScoreboardItem.Include(u => u.UserProfile)
                 .Select(item => new SelectListItem()
                 {
                     Text = item.Title + " - " + item.UserProfile.FullName,
                     Value = item.ScoreboardItemId.ToString()
                 });

            return mySelects.ToList();
        }
    }
}
