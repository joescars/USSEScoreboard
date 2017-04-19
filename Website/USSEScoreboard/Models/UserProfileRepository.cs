using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace USSEScoreboard.Models
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<UserProfile>> GetUserProfilesAsync()
        {
            return await _context.UserProfile.ToListAsync();
        }
    }
}
