using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using USSEScoreboard.Data;
using USSEScoreboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using USSEScoreboard.Models.CommitmentViewModels;

namespace USSEScoreboard.Controllers
{
    [Authorize]
    public class CommitmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommitmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;    
            _userManager = userManager;
        }

        // GET: Commitments
        public async Task<IActionResult> Index()
        {
            var model = new ListCommitmentsViewModel();
            model.Commitments = await _context.Commitment
                .Include(u => u.UserProfile)
                .OrderByDescending(u => u.DateCreated).ToListAsync();
            return View(model);
        }

        // GET: Committments by User
        public async Task<IActionResult> SearchByUser(int id)
        {
            var model = new ListCommitmentsViewModel();
            model.Commitments = await _context.Commitment
                .Where(u => u.UserProfileId == id)
                .Include(u => u.UserProfile)
                .OrderByDescending(u => u.DateCreated)
                .ToListAsync();

            model.SearchUserName = await _context.UserProfile
                .Where(u => u.UserProfileId == id)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();

            return View(model);
        }

        // GET: My Commitments (Logged in User)
        public async Task<ActionResult> My()
        {
            var userId = _userManager.GetUserId(User);
            var model = new ListCommitmentsViewModel();

            model.Commitments = await _context.Commitment
                .Where(u => u.UserProfile.UserId == userId)
                .Include(u => u.UserProfile)
                .OrderByDescending(u => u.DateCreated).ToListAsync();

            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserId == userId);

            model.IsCRM = up.IsCRM;
            model.IsExpenses = up.IsExpenses;

            return View(model);
        }

        // GET: Commitments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commitment = await _context.Commitment.SingleOrDefaultAsync(m => m.Id == id);
            if (commitment == null)
            {
                return NotFound();
            }

            return View(commitment);
        }

        // GET: Commitments/Create
        public async Task<ViewResult> Create()
        {
            var model = new CreateCommitmentViewModel();
            model.Users = await _context.UserProfile.ToListAsync();
            model.Wigs = await _context.Wig.ToListAsync();
            //Get the userId of the logged in user so we can default to this
            var userId = _userManager.GetUserId(User);
            model.SelectedUserID = model.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.UserProfileId).First();
            return View(model);
        }

        // POST: Commitments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Status,Title,SelectedUserID,SelectedWigId")] CreateCommitmentViewModel model)
        {
            //TODO: Remove CommitmentViewModel as it is no longer needed
            if (ModelState.IsValid)
            {
                var commitment = new Commitment();
                commitment.DateCreated = DateTime.Now;
                commitment.Description = model.Description;
                commitment.Status = model.Status;
                commitment.Title = model.Title;
                commitment.UserProfileId = model.SelectedUserID;
                commitment.WigId = model.SelectedWigId;
                _context.Add(commitment);
                await _context.SaveChangesAsync();
                return RedirectToAction("My");

            }
            return View(model);
        }

        // GET: Commitments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new EditCommitmentViewModel();

            var commitment = await _context.Commitment
                .Include(u => u.UserProfile)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (commitment == null)
            {
                return NotFound();
            }

            model.Id = commitment.Id;
            model.Title = commitment.Title;
            model.Description = commitment.Description;
            model.Status = commitment.Status;
            model.SelectedUserID = commitment.UserProfileId;
            model.SelectedWigId = commitment.WigId;
            model.DateCreated = commitment.DateCreated;
            model.Users = await _context.UserProfile.ToListAsync();
            model.Wigs = await _context.Wig.ToListAsync();

            return View(model);
        }

        // POST: Commitments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,DateCreated,Description,Status,Title")] Commitment commitment,
            int SelectedUserId, int SelectedWigId)
        {
            if (id != commitment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    commitment.UserProfileId = SelectedUserId;
                    commitment.WigId = SelectedWigId;
                    commitment.DateModified = DateTime.Now;
                    _context.Update(commitment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommitmentExists(commitment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("My");
            }
            return View(commitment);
        }

        // GET: Commitments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commitment = await _context.Commitment.SingleOrDefaultAsync(m => m.Id == id);
            if (commitment == null)
            {
                return NotFound();
            }

            return View(commitment);
        }

        // POST: Commitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commitment = await _context.Commitment.SingleOrDefaultAsync(m => m.Id == id);
            _context.Commitment.Remove(commitment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Commitments/ToggleExpenses
        [HttpGet]
        public async Task<IActionResult> ToggleExpenses()
        {
            var userId = _userManager.GetUserId(User);
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserId == userId);
            up.IsExpenses = !up.IsExpenses;
            await _context.SaveChangesAsync();
            return RedirectToAction("My");
        }

        // GET: Commitments/ToggleCRM
        [HttpGet]
        public async Task<IActionResult> ToggleCRM()
        {
            var userId = _userManager.GetUserId(User);
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserId == userId);
            up.IsCRM = !up.IsCRM;
            await _context.SaveChangesAsync();
            return RedirectToAction("My");
        }

        private bool CommitmentExists(int id)
        {
            return _context.Commitment.Any(e => e.Id == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private Task<IList<ApplicationUser>> GetUsersAsync()
        {
            return null;
        }

        private Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetCommitmentsList()
        {
            //var myList = _context.Commitment.Include(u => u.UserProfile).ToListAsync();
            //return Json(myList);

            // Used LINQ to create custom query with data we need
            var commitmentsList = (from c in _context.Commitment
                                   select new
                                   {
                                       c.Id,
                                       c.Title,
                                       c.Description,
                                       c.Status,
                                       c.DateCreated,
                                       c.UserProfile.FullName
                                   });

            return Json(commitmentsList);
        }

    }
}
