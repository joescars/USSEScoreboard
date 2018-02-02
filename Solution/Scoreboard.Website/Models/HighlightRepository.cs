using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scoreboard.Website.Interfaces;
using Scoreboard.Website.Data;
using Scoreboard.Website.Models;
using Microsoft.EntityFrameworkCore;

namespace Scoreboard.Website.Models
{
    public class HighlightRepository : IHighlightRepository
    {
        private ApplicationDbContext _context;

        public HighlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Highlight> GetHighlightByIdAsync(int? id)
        {
            return await _context.Highlight
                .Include(u => u.UserProfile)
                .SingleOrDefaultAsync(m => m.HighlightId == id);
        }

        public async Task<List<HighlightListItem>> GetHighlightsAsync()
        {
            return await _context.Highlight
                .Include(h => h.UserProfile)
                .Select(h => new HighlightListItem
                {
                    HighlightId = h.HighlightId,
                    FullName = h.UserProfile.FullName,
                    DateStart = h.DateStart,
                    DateEnd = h.DateEnd,
                    DateCreated = h.DateCreated
                })
                .OrderByDescending(h => h.DateCreated).ToListAsync();
        }

        public async Task<List<HighlightListItem>> GetHighlightsByUserId(string userId)
        {
            return await _context.Highlight
                .Include(h => h.UserProfile)
                .Where(u => u.UserProfile.UserId == userId)
                .Select(h => new HighlightListItem
                {
                    HighlightId = h.HighlightId,
                    FullName = h.UserProfile.FullName,
                    DateStart = h.DateStart,
                    DateEnd = h.DateEnd,
                    DateCreated = h.DateCreated
                })
                .OrderByDescending(h => h.DateCreated).ToListAsync();
        }

        public async Task<List<HighlightListItem>> GetHighlightsByUserProfileId(int userProfileId)
        {
            return await _context.Highlight
                .Include(h => h.UserProfile)
                .Where(h => h.UserProfileId == userProfileId)
                .Select(h => new HighlightListItem
                {
                    HighlightId = h.HighlightId,
                    FullName = h.UserProfile.FullName,
                    DateStart = h.DateStart,
                    DateEnd = h.DateEnd,
                    DateCreated = h.DateCreated
                })
                .OrderByDescending(h => h.DateCreated).ToListAsync();
        }

        public async Task<List<HighlightSearchResult>> GetHighlightsByDateRange(DateTime start, DateTime end)
        {
            return await _context.Highlight
                .Include(h => h.UserProfile)
                .Where(h => h.DateStart >= start && h.DateEnd <= end)
                .Select(h => new HighlightSearchResult
                {
                    HighlightId = h.HighlightId,
                    FullName = h.UserProfile.FullName,
                    Body = h.Body,
                    DateStart = h.DateStart,
                    DateEnd = h.DateEnd,
                    DateCreated = h.DateCreated
                })
                .OrderBy(h => h.DateCreated)
                .OrderBy(h => h.FullName).ToListAsync();
        }

        public async Task SaveHighlightAsync(Highlight h, int userProfileId)
        {
            h.UserProfileId = userProfileId;
            h.DateCreated = DateTime.Now;
            h.DateModified = DateTime.Now;
            _context.Add(h);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHighlightAsync(Highlight h)
        {
            h.DateModified = DateTime.Now;
            _context.Update(h);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHighlightAsync(Highlight h)
        {
            _context.Remove(h);
            await _context.SaveChangesAsync();
        }

        public bool HighlightExists(int id)
        {
            return _context.Highlight.Any(e => e.HighlightId == id);
        }

        public DateTime GetStartOfWeek()
        {
            var dt = DateTime.Now;
            int diff = dt.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
