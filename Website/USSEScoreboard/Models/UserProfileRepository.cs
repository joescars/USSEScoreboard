using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace USSEScoreboard.Models
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetUserProfileByUserProfileIdAsync(int userProfileId)
        {
            return await _context.UserProfile
                .Where(u => u.UserProfileId == userProfileId)
                .SingleOrDefaultAsync();
        }

        public async Task<UserProfile> GetUserProfileByUserIdAsync(string userId)
        {
            return await _context.UserProfile
                .Where(u => u.UserId == userId)
                .SingleOrDefaultAsync();
        }

        public string GetUserProfileFirstName(string userId)
        {
            return _context.UserProfile
                .Where(u => u.UserId == userId)
                .SingleOrDefault().FirstName;
        }

        public async Task<int> GetUserPofileIdByUserIdAsync(string userId)
        {
            return await _context.UserProfile
                .Where(u => u.UserId == userId)
                .Select(u => u.UserProfileId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserProfile>> GetUserProfilesAsync()
        {
            return await _context.UserProfile.ToListAsync();
        }

        public async Task<bool> ValidateUserProfileAsync(IEnumerable<Claim> claims)
        {            
            var firstName = GetClaimValue(claims, "/claims/givenname");
            var lastName = GetClaimValue(claims, "/claims/surname");
            var objectId = GetClaimValue(claims, "/claims/objectidentifier");
            var email = GetClaimValue(claims, "/claims/name");

            // lets first lookup by the *objectId*
            if (!_context.UserProfile.Any(u => u.UserId == objectId))
            {
                // if its not found, lets then lookup by *first* and *last* name
                if (_context.UserProfile.Any(u => u.FirstName == firstName && u.LastName == lastName))
                {
                    // profile found, update record if needed
                    UserProfile up = await _context.UserProfile
                        .Where(u => u.FirstName == firstName && u.LastName == lastName).FirstAsync();

                    up.UserId = objectId;
                    up.EmailAddress = email;
                    up.DateModified = DateTime.Now;
                    await _context.SaveChangesAsync();

                }
                else
                {
                    // if not found, create a new userprofile record
                    UserProfile up = new UserProfile
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        EmailAddress = email,
                        UserId = objectId
                    };
                    _context.UserProfile.Add(up);
                    await _context.SaveChangesAsync();
                }

            }
            
            // if userid found we are all set, do nothing        
            // we are done
            return true;

        }

        private string GetClaimValue(IEnumerable<Claim> claims, string type)
        {
            return claims.Where(x => x.Type.EndsWith(type)).First().Value;
        }
    }
}
