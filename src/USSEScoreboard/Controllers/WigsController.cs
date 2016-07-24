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
using USSEScoreboard.Models.WigViewModels;

namespace USSEScoreboard.Controllers
{
    [Authorize]
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
        public async Task<ViewResult> Create()
        {
            var model = new CreateWigViewModel();
            model.UserProfiles = await _context.UserProfile.ToListAsync();
            return View(model);
        }

        // POST: Wigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WigId,Description,Status,Title,SelectedUserProfiles")] CreateWigViewModel model)
        {
            if (ModelState.IsValid)
            {
                var wig = new Wig();
                wig.Title = model.Title;
                wig.Description = model.Description;
                wig.Status = model.Status;
                wig.DateCreated = DateTime.Now;
                _context.Add(wig);

                if (model.SelectedUserProfiles != null)
                {
                    foreach (var i in model.SelectedUserProfiles)
                    {
                        var uw = new UserWig();
                        uw.WigId = wig.WigId;
                        uw.UserProfileId = i;
                        _context.Add(uw);
                    }
                }
                                
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Wigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get source WIG            
            var wig = await _context.Wig.Include(u => u.UserWigs).SingleOrDefaultAsync(m => m.WigId == id);
            if (wig == null)
            {
                return NotFound();
            }

            // Create view model and populate
            var model = new EditWigViewModel();
            model.WigId = wig.WigId;
            model.Title = wig.Title;
            model.Description = wig.Description;
            model.Status = wig.Status;
            model.DateCreated = wig.DateCreated;
            model.SelectedUserProfiles = wig.UserWigs.Select(m => m.UserProfileId).ToList();
            model.UserProfiles = await _context.UserProfile.ToListAsync();
            return View(model);
        }

        // POST: Wigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("WigId,DateCreated,Description,Status,Title")] Wig wig,
            List<int> SelectedUserProfiles)
        {

            if (id != wig.WigId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the Source Wig with corresponding user profile data
                    var myWig = await _context.Wig
                        .Include(u => u.UserWigs)
                        .SingleOrDefaultAsync(m => m.WigId == id);

                    myWig.Title = wig.Title;
                    myWig.Description = wig.Description;
                    myWig.Status = wig.Status;

                    //removes users that are already there and not in new list
                    foreach (UserWig uw in myWig.UserWigs.ToList())
                    {
                        if (!SelectedUserProfiles.Contains(uw.UserProfileId))
                            myWig.UserWigs.Remove(uw);
                    }

                    // Add the new ones
                    foreach (int i in SelectedUserProfiles)
                    {
                        if (!myWig.UserWigs.Any(u=> u.UserProfileId == i))
                        {
                            var newUserWig = new UserWig { UserProfileId = i, WigId = id };
                            myWig.UserWigs.Add(newUserWig);
                        }

                    }

                    // Save Changes
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
            //modified to include UserWigs
            var wig = await _context.Wig
                .Include(u => u.UserWigs)
                .SingleOrDefaultAsync(m => m.WigId == id);

            //removes associated wigs
            wig.UserWigs.Clear();

            _context.Wig.Remove(wig);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WigExists(int id)
        {
            return _context.Wig.Any(e => e.WigId == id);
        }

        public ActionResult GetWigsList()
        {
            //var myList = _context.Commitment.Include(u => u.UserProfile).ToListAsync();
            //return Json(myList);

            // Used LINQ to create custom query with data we need
            var WigsList = (from c in _context.Commitment
                                   select new
                                   {
                                       c.Id,
                                       c.Title,
                                       c.Description,
                                       c.Status,
                                       c.DateCreated,
                                       c.UserProfile.FullName
                                   });

            return Json(WigsList);
        }

    }
}
