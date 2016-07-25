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
    public class PresentationEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresentationEntriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PresentationEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PresentationEntry.Include(p => p.UserProfile)
                .OrderByDescending(p => p.DateCreated);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PresentationEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentationEntry = await _context.PresentationEntry.SingleOrDefaultAsync(m => m.PresentationEntryId == id);
            if (presentationEntry == null)
            {
                return NotFound();
            }

            return View(presentationEntry);
        }

        // GET: PresentationEntries/Create
        public IActionResult Create()
        {
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FullName");
            return View();
        }

        // POST: PresentationEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresentationEntryId,DateCreated,DateModified,Total,UserProfileId,WeekEnding")] PresentationEntry presentationEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(presentationEntry);

                var up = await _context.UserProfile.Where(u => u.UserProfileId == presentationEntry.UserProfileId).FirstOrDefaultAsync();
                up.TotalPresentations += presentationEntry.Total;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", presentationEntry.UserProfileId);
            return View(presentationEntry);
        }

        // GET: PresentationEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentationEntry = await _context.PresentationEntry.SingleOrDefaultAsync(m => m.PresentationEntryId == id);
            if (presentationEntry == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", presentationEntry.UserProfileId);
            return View(presentationEntry);
        }

        // POST: PresentationEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresentationEntryId,DateCreated,DateModified,Total,UserProfileId,WeekEnding")] PresentationEntry presentationEntry)
        {
            if (id != presentationEntry.PresentationEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presentationEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentationEntryExists(presentationEntry.PresentationEntryId))
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
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "UserProfileId", presentationEntry.UserProfileId);
            return View(presentationEntry);
        }

        // GET: PresentationEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentationEntry = await _context.PresentationEntry.SingleOrDefaultAsync(m => m.PresentationEntryId == id);
            if (presentationEntry == null)
            {
                return NotFound();
            }

            return View(presentationEntry);
        }

        // POST: PresentationEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presentationEntry = await _context.PresentationEntry
                .Include(u => u.UserProfile)
                .SingleOrDefaultAsync(m => m.PresentationEntryId == id);

            //Update the userprofile total
            presentationEntry.UserProfile.TotalPresentations -= presentationEntry.Total;

            _context.PresentationEntry.Remove(presentationEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PresentationEntryExists(int id)
        {
            return _context.PresentationEntry.Any(e => e.PresentationEntryId == id);
        }
    }
}
