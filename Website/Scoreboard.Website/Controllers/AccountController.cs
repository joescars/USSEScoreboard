using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using USSEScoreboard.Models;
using USSEScoreboard.Models.AccountViewModels;
using USSEScoreboard.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using USSEScoreboard.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public AccountController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // GET: /Account/Login
        [HttpGet]
        public async Task Login()
        {
            if (HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
                await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/Account/PostLogin" });
           
        }

        // GET: /Account/LogOff
        [HttpGet]
        public async Task LogOff()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        [HttpGet]
        public async Task EndSession()
        {
            // If AAD sends a single sign-out message to the app, end the user's session, but don't redirect to AAD for sign out.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> PostLogin()
        {
            // check to see if user exists in local store
            // if yes, redirect
            // if no, create profile, then redirect 

            await _userProfileRepository.ValidateUserProfileAsync(User.Claims);

            return RedirectToAction("index", "home");
        }

    }
}
