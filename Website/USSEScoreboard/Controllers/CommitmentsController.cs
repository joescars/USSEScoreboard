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
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Controllers
{
    [Authorize]
    public class CommitmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToggleService _toggleService;
        private readonly ICommitmentRepository _commitmentRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public CommitmentsController(ApplicationDbContext context,             
            IToggleService toggleService,
            ICommitmentRepository commitmentRespository,
            IUserProfileRepository userProfileRepository)
        {
            _context = context;    
            //_userManager = userManager;
            _toggleService = toggleService;
            _commitmentRepository = commitmentRespository;
            _userProfileRepository = userProfileRepository;
        }

        // GET: Commitments
        public async Task<IActionResult> Index()
        {
            var model = new ListCommitmentsViewModel();
            model.Commitments = await _commitmentRepository.GetCommitmentsAsync();
            return View(model);
        }

        // GET: Committments by User
        public async Task<IActionResult> SearchByUser(int id)
        {
            var userId = ""; //TODO
            var userProfileId = await _userProfileRepository.GetUserPofileIdByUserIdAsync(userId);

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
                model.Commitments = await _commitmentRepository.GetCommitmentsByUserProfileAsync(id);

                var u = await _userProfileRepository.GetUserProfileByUserProfileIdAsync(id);
                model.SearchUserName = u.FirstName;

                return View(model);
            }
           
        }

        // GET: My Commitments (Logged in User)
        public async Task<ActionResult> My()
        {
            var userId = ""; //TODO
            var model = new ListCommitmentsViewModel();

            model.Commitments = await _commitmentRepository.GetCommitmentsByUserAsync(userId);

            var up = await _userProfileRepository.GetUserProfileByUserIdAsync(userId);

            model.IsCRM = up.IsCRM;
            model.IsExpenses = up.IsExpenses;
            model.IsFRI = up.IsFRI;
            model.IsAscendNotes = up.IsAscendNotes;
            model.UserProfileId = up.UserProfileId;

            return View(model);
        }

        // GET: Commitments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commitment = await _commitmentRepository.GetCommitmentAsync(id);
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
            model.Users = await _userProfileRepository.GetUserProfilesAsync();
            model.Wigs = await _context.Wig.ToListAsync();
            model.LeadMeasures = await _context.LeadMeasure
                .Include(w => w.Wig)
                .ToListAsync();
            //Get the userId of the logged in user so we can default to this
            var userId = ""; //TODO
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

                await _commitmentRepository.SaveCommitmentAsync(commitment);
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

            var commitment = await _commitmentRepository.GetCommitmentAsync(id);
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
            model.Users = await _userProfileRepository.GetUserProfilesAsync();
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

                    await _commitmentRepository.UpdateCommitmentAsync(commitment);
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

            var commitment = await _commitmentRepository.GetCommitmentAsync(id);
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
            var commitment = await _commitmentRepository.GetCommitmentAsync(id);
            await _commitmentRepository.DeleteCommitmentAsync(commitment);
            return RedirectToAction("Index");
        }

        // GET: Commitments/Complete/1
        public async Task<IActionResult> Complete(int? id)
        {
            await _commitmentRepository.MarkComplete(id);
            return RedirectToAction("My");
        }

        // GET: Commitments/ToggleExpensesUser/1
        [HttpGet]
        public async Task<IActionResult> ToggleExpensesUser(int id)
        {
            await _toggleService.ToggleUserExpense(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
       
        // GET: Commitments/ToggleCRMUser/1 (userprofiled)
        [HttpGet]
        public async Task<IActionResult> ToggleCRMUser(int id)
        {
            await _toggleService.ToggleUserCRM(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Commitments/ToggleFRIUser/1
        [HttpGet]
        public async Task<IActionResult> ToggleFRIUser(int id)
        {
            await _toggleService.ToggleUserFRI(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Commitments/ToggleAscendNotes/User/1
        [HttpGet]
        public async Task<IActionResult> ToggleAscendNotesUser(int id)
        {
            await _toggleService.ToggleUserAscendNotes(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool CommitmentExists(int id)
        {
            return _commitmentRepository.CommitmentExists(id);
        }

        //private Task<ApplicationUser> GetCurrentUserAsync()
        //{
        //    return _userManager.GetUserAsync(HttpContext.User);
        //}

        //private Task<IList<ApplicationUser>> GetUsersAsync()
        //{
        //    return null;
        //}

        //private Task<ApplicationUser> GetUserByIdAsync(string id)
        //{
        //    var user = _userManager.FindByIdAsync(id);
        //    if (user != null)
        //    {
        //        return user;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        // TODO: Confirm this is no longer used and delete

        public ActionResult GetCommitmentsList()
        {
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
