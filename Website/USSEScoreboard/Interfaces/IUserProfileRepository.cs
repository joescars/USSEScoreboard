using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Models;

namespace USSEScoreboard.Interfaces
{
    public interface IUserProfileRepository
    {
        string GetUserProfileFirstName(string userId);
        Task<UserProfile> GetUserProfileByUserIdAsync(string userId);
        Task<IEnumerable<UserProfile>> GetUserProfilesAsync();
    }
}
