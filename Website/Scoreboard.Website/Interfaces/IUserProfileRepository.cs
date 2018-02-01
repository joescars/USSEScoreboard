using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using USSEScoreboard.Models;

namespace USSEScoreboard.Interfaces
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
