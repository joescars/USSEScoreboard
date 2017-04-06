using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;

namespace USSEScoreboard.Services
{
    public class ToggleService : IToggleService
    {
        private readonly ApplicationDbContext _context;
        
        public ToggleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ToggleUserCRM(int userId)
        {
            var up = await _context.UserProfile
                .SingleOrDefaultAsync(u => u.UserProfileId == userId);
            up.IsCRM = !up.IsCRM;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
