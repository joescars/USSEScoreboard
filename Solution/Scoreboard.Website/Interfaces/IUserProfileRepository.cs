using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Scoreboard.Website.Models;
using Scoreboard.Common.Entities;

namespace Scoreboard.Website.Interfaces
{
    public interface IUserProfileRepository
    {
        string GetUserProfileFirstName(string userId);
        Task<int> GetUserPofileIdByUserIdAsync(string userId);
        Task<UserProfile> GetUserProfileByUserIdAsync(string userId);
        Task<UserProfile> GetUserProfileByUserProfileIdAsync(int userProfileId);
        Task<IEnumerable<UserProfile>> GetUserProfilesAsync();
        Task<bool> ValidateUserProfileAsync(IEnumerable<Claim> claims);
    }
}
