using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Models;
using USSEScoreboard.Interfaces;

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
            var up = await GetUserProfileById(userId);
            up.IsCRM = !up.IsCRM;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task ToggleUserExpense(int userId)
        {
            var up = await GetUserProfileById(userId);
            up.IsExpenses = !up.IsExpenses;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task ToggleUserFRI(int userId)
        {
            var up = await GetUserProfileById(userId);
            up.IsFRI = !up.IsFRI;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task ToggleUserAscendNotes(int userId)
        {
            var up = await GetUserProfileById(userId);
            up.IsAscendNotes = !up.IsAscendNotes;
            up.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        private async Task<UserProfile> GetUserProfileById(int id)
        {
            return await _context.UserProfile
                .SingleOrDefaultAsync(u => u.UserProfileId == id);
        }
    }
}
