using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Models;

namespace USSEScoreboard.Interfaces
{
    public interface IHighlightRepository
    {
        Task<List<HighlightListItem>> GetHighlightsAsync();
        Task<List<HighlightListItem>> GetHighlightsByUserId(string userId);
        Task<List<HighlightSearchResult>> GetHighlightsByDateRange(DateTime start, DateTime end);
        Task<Highlight> GetHighlightByIdAsync(int? id);
        Task SaveHighlightAsync(Highlight h, int userProfileId);
        Task UpdateHighlightAsync(Highlight h);
        Task DeleteHighlightAsync(Highlight h);
        bool HighlightExists(int id);
    }
}
