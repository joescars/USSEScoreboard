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
            var userId = _userManager.GetUserId(User);
            var userProfileId = await _context.UserProfile
                .Where(u => u.UserId == userId)
                .Select(u => u.UserProfileId).FirstOrDefaultAsync();
            // check if searching for logged in user
            if (userProfileId == id)
            {
                // redirect to my commitments
                return RedirectToAction("My");
            }
            else
            {
                // return search results of other user
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
            model.IsFRI = up.IsFRI;

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
            model.LeadMeasures = await _context.LeadMeasure
                .Include(w => w.Wig)
                .ToListAsync();
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
        public async Task<IActionResult> Create([Bind("Id,Description,Status,Title,SelectedUserID,SelectedLeadMeasureId")] CreateCommitmentViewModel model)
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
                commitment.LeadMeasureId = model.SelectedLeadMeasureId;
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
            model.SelectedLeadMeasureId = commitment.LeadMeasureId;
            model.DateCreated = commitment.DateCreated;
            model.Users = await _context.UserProfile.ToListAsync();
            model.LeadMeasures = await _context.LeadMeasure
                .Include(w => w.Wig)
                .ToListAsync();

            return View(model);
        }

        // POST: Commitments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,DateCreated,Description,Status,Title")] Commitment commitment,
            int SelectedUserId, int SelectedLeadMeasureId)
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
                    commitment.LeadMeasureId = SelectedLeadMeasureId;
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

        // GET: Commitments/Complete/1
        public async Task<IActionResult> Complete(int? id)
        {
            var commitment = await _context.Commitment.SingleOrDefaultAsync(m => m.Id == id);
            commitment.Status = CommitmentStatus.Complete;
            commitment.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("My");
        }

        /***************************************************************
           TODO: Refactor toggles into one method called from service
        ***************************************************************/

        // GET: Commitments/ToggleExpenses
        [HttpGet]
        public async Task<IActionResult> ToggleExpenses()
        {
            var userId = _userManager.GetUserId(User);
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserId == userId);
            up.IsExpenses = !up.IsExpenses;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("My");
        }

        // GET: Commitments/ToggleExpensesUser/1
        [HttpGet]
        public async Task<IActionResult> ToggleExpensesUser(int id)
        {
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserProfileId == id);
            up.IsExpenses = !up.IsExpenses;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        
        // GET: Commitments/ToggleCRM
        [HttpGet]
        public async Task<IActionResult> ToggleCRM()
        {
            var userId = _userManager.GetUserId(User);
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserId == userId);
            up.IsCRM = !up.IsCRM;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("My");
        }

        
        // GET: Commitments/ToggleCRMUser/1 (userprofiled)
        [HttpGet]
        public async Task<IActionResult> ToggleCRMUser(int id)
        {
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserProfileId == id);
            up.IsCRM = !up.IsCRM;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        // GET: Commitments/ToggleFRIUser/1
        [HttpGet]
        public async Task<IActionResult> ToggleFRI()
        {
            var userId = _userManager.GetUserId(User);
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserId == userId);
            up.IsFRI = !up.IsFRI;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("My");
        }

        [HttpGet]
        public async Task<IActionResult> ToggleFRIUser(int id)
        {
            var up = await _context.UserProfile.SingleOrDefaultAsync(u => u.UserProfileId == id);
            up.IsFRI = !up.IsFRI;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
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
