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
    public class LeadMeasuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadMeasuresController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: LeadMeasures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeadMeasure.Include(l => l.Wig);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeadMeasures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadMeasure = await _context.LeadMeasure.SingleOrDefaultAsync(m => m.LeadMeasureId == id);
            if (leadMeasure == null)
            {
                return NotFound();
            }

            return View(leadMeasure);
        }

        // GET: LeadMeasures/Create
        public IActionResult Create()
        {
            ViewData["WigId"] = new SelectList(_context.Wig, "WigId", "Title");
            return View();
        }

        // POST: LeadMeasures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeadMeasureId,DateCreated,DateModified,Description,WigId")] LeadMeasure leadMeasure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leadMeasure);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["WigId"] = new SelectList(_context.Wig, "WigId", "Title", leadMeasure.WigId);
            return View(leadMeasure);
        }

        // GET: LeadMeasures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadMeasure = await _context.LeadMeasure.SingleOrDefaultAsync(m => m.LeadMeasureId == id);
            if (leadMeasure == null)
            {
                return NotFound();
            }
            ViewData["WigId"] = new SelectList(_context.Wig, "WigId", "Title", leadMeasure.WigId);
            return View(leadMeasure);
        }

        // POST: LeadMeasures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeadMeasureId,DateCreated,Description,WigId")] LeadMeasure leadMeasure)
        {
            if (id != leadMeasure.LeadMeasureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    leadMeasure.DateModified = DateTime.Now;
                    _context.Update(leadMeasure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadMeasureExists(leadMeasure.LeadMeasureId))
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
            ViewData["WigId"] = new SelectList(_context.Wig, "WigId", "Title", leadMeasure.WigId);
            return View(leadMeasure);
        }

        // GET: LeadMeasures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadMeasure = await _context.LeadMeasure.SingleOrDefaultAsync(m => m.LeadMeasureId == id);
            if (leadMeasure == null)
            {
                return NotFound();
            }

            return View(leadMeasure);
        }

        // POST: LeadMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leadMeasure = await _context.LeadMeasure.SingleOrDefaultAsync(m => m.LeadMeasureId == id);
            _context.LeadMeasure.Remove(leadMeasure);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LeadMeasureExists(int id)
        {
            return _context.LeadMeasure.Any(e => e.LeadMeasureId == id);
        }
    }
}
