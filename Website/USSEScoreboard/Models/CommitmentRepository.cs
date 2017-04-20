using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Interfaces;
using USSEScoreboard.Models.CommitmentViewModels;

namespace USSEScoreboard.Models
{
    public class CommitmentRepository : ICommitmentRepository
    {
        private ApplicationDbContext _context;

        public CommitmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Commitment> GetCommitmentAsync(int? id)
        {
            return await _context.Commitment
                .Include(u => u.UserProfile)
                .SingleOrDefaultAsync(m => m.Id == id);
        }


        public async Task<IEnumerable<Commitment>> GetCommitmentsAsync()
        {
            return await _context.Commitment
                .Include(u => u.UserProfile)
                .OrderByDescending(u => u.DateCreated).ToListAsync();
        }

        public async Task SaveCommitmentAsync(Commitment c)
        {
            _context.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommitmentAsync(Commitment c)
        {
            c.DateModified = DateTime.Now;

            _context.Update(c);
            await _context.SaveChangesAsync();
        }

        public async Task MarkComplete(int? id)
        {
            var c = await GetCommitmentAsync(id);
            c.Status = CommitmentStatus.Complete;
            await UpdateCommitmentAsync(c);
        }

        public async Task<IEnumerable<Commitment>> GetCommitmentsByUserAsync(string userId)
        {
            return await _context.Commitment
                .Where(u => u.UserProfile.UserId == userId)
                .Include(u => u.UserProfile)
                .OrderByDescending(u => u.DateCreated).ToListAsync();
        }

        public async Task<IEnumerable<Commitment>> GetCommitmentsByUserProfileAsync(int userProfileId)
        {
            return await _context.Commitment
                    .Where(u => u.UserProfileId == userProfileId)
                    .Include(u => u.UserProfile)
                    .OrderByDescending(u => u.DateCreated)
                    .ToListAsync();
        }

        public async Task DeleteCommitmentAsync(Commitment c)
        {
            _context.Remove(c);
            await _context.SaveChangesAsync();
        }

        public bool CommitmentExists(int id)
        {
            return _context.Commitment.Any(e => e.Id == id);
        }
    }
}
