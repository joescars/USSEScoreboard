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
    public class WigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WigsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Wigs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wig.ToListAsync());
        }

        // GET: Wigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wig = await _context.Wig.SingleOrDefaultAsync(m => m.WigId == id);
            if (wig == null)
            {
                return NotFound();
            }

            return View(wig);
        }

        // GET: Wigs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WigId,Description,Status,Title")] Wig wig)
        {
            if (ModelState.IsValid)
            {
                wig.DateCreated = DateTime.Now;
                _context.Add(wig);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wig);
        }

        // GET: Wigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wig = await _context.Wig.SingleOrDefaultAsync(m => m.WigId == id);
            if (wig == null)
            {
                return NotFound();
            }
            return View(wig);
        }

        // POST: Wigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WigId,DateCreated,Description,Status,Title")] Wig wig)
        {
            if (id != wig.WigId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WigExists(wig.WigId))
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
            return View(wig);
        }

        // GET: Wigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wig = await _context.Wig.SingleOrDefaultAsync(m => m.WigId == id);
            if (wig == null)
            {
                return NotFound();
            }

            return View(wig);
        }

        // POST: Wigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wig = await _context.Wig.SingleOrDefaultAsync(m => m.WigId == id);
            _context.Wig.Remove(wig);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WigExists(int id)
        {
            return _context.Wig.Any(e => e.WigId == id);
        }
    }
}
