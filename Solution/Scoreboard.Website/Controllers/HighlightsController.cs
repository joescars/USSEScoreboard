using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scoreboard.Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Scoreboard.Website.Interfaces;
using System.Security.Claims;
using Scoreboard.Website.Models.HighlightViewModels;
using Scoreboard.Common.Entities;

namespace Scoreboard.Website.Controllers
{
    [Authorize]
    public class HighlightsController : Controller
    {        
        private readonly IHighlightRepository _highlightRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public HighlightsController(
            IHighlightRepository highlightrepository,
            IUserProfileRepository userProfileRepository)
        {            
            _highlightRepository = highlightrepository;
            _userProfileRepository = userProfileRepository;
        }

        // GET: Highlights
        public async Task<IActionResult> Index()
        {
            var model = new ListHighlightsViewModel();
            model.Highlights = await _highlightRepository.GetHighlightsAsync();
            var up = await _userProfileRepository.GetUserProfilesAsync();
            var sList = new SelectList(up, "UserProfileId", "FullName");
            model.ActiverUserList = sList;
            return View(model);
        }

        // GET: Highlights/My
        public async Task<IActionResult> My()
        {
            // Get User Profile
            // TODO Clean this up
            var userId = User.Claims
                .Where(x => x.Type.EndsWith("/claims/objectidentifier")).First().Value;
            var model = new ListHighlightsViewModel();
            model.Highlights = await _highlightRepository.GetHighlightsByUserId(userId);
            var up = await _userProfileRepository.GetUserProfilesAsync();
            //var sList = new SelectList(up, "UserProfileId", "FullName");
            //model.ActiverUserList = sList;
            return View(model);
        }

        public async Task<IActionResult> ByUser(int id)
        {
            // TODO: Refactor as we are duplicating code;
            var model = new ListHighlightsViewModel();
            model.Highlights = await _highlightRepository.GetHighlightsByUserProfileId(id);
            var up = await _userProfileRepository.GetUserProfilesAsync();
            //var sList = new SelectList(up, "UserProfileId", "FullName");
            //model.ActiverUserList = sList;
            return View(model);
        }

        // GET: Highlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }

            return View(highlight);
        }

        // GET: Highlights/Create
        public IActionResult Create()
        {
            var model = new Highlight();
            var WeekStart = _highlightRepository.GetStartOfWeek();
            model.DateStart = WeekStart;
            model.DateEnd = WeekStart.AddDays(4);
            return View(model);
        }

        // POST: Highlights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HighlightId,Body,DateEnd,DateStart,UserProfileId")] Highlight highlight)
        {
            if (ModelState.IsValid)
            {
                // Get User Profile
                // TODO Clean this up
                var userId = User.Claims
                    .Where(x => x.Type.EndsWith("/claims/objectidentifier")).First().Value;
                var up = await _userProfileRepository
                    .GetUserProfileByUserIdAsync(userId);

                await _highlightRepository.SaveHighlightAsync(highlight, up.UserProfileId);
                return RedirectToAction("Index");
            }
            return View(highlight);
        }

        // GET: Highlights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(await _userProfileRepository.GetUserProfilesAsync(), "UserProfileId", "FullName", highlight.UserProfileId);
            return View(highlight);
        }

        // POST: Highlights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HighlightId,Body,DateCreated,DateEnd,DateModified,DateStart,UserProfileId")] Highlight highlight)
        {
            if (id != highlight.HighlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _highlightRepository.UpdateHighlightAsync(highlight);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_highlightRepository.HighlightExists(highlight.HighlightId))
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
            return View(highlight);
        }

        // GET: Highlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }

            return View(highlight);
        }

        // POST: Highlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var highlight = await _highlightRepository.GetHighlightByIdAsync(id);
            await _highlightRepository.DeleteHighlightAsync(highlight);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(DateTime startDate, DateTime endDate)
        {
            return View(await _highlightRepository.GetHighlightsByDateRange(startDate, endDate));
        }

    }
}
