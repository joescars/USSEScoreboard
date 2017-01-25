using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Models
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UserProfile GetUserProfileByUserIdAsync(string userId)
        {
            return _dbContext.UserProfile
                .Where(u => u.UserId == userId)
                .FirstOrDefault();
        }
    }
}
