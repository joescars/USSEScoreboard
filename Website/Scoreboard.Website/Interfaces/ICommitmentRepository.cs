using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Models;
using USSEScoreboard.Models.CommitmentViewModels;

namespace USSEScoreboard.Interfaces
{
    public interface ICommitmentRepository
    {
        Task<Commitment> GetCommitmentAsync(int? id);
        Task<IEnumerable<Commitment>> GetCommitmentsAsync();
        Task SaveCommitmentAsync(Commitment c);
        Task UpdateCommitmentAsync(Commitment c);
        Task MarkComplete(int? id);
        Task DeleteCommitmentAsync(Commitment c);
        bool CommitmentExists(int id);
        Task<IEnumerable<Commitment>> GetCommitmentsByUserAsync(string userId);
        Task<IEnumerable<Commitment>> GetCommitmentsByUserProfileAsync(int userProfileId);
    }
}
