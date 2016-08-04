using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models.CommitmentViewModels
{
    public class ListCommitmentsViewModel
    {
        public IEnumerable<Commitment> Commitments { get; set; }
        public bool IsCRM { get; set; }
        public bool IsExpenses { get; set; }
        public bool IsFRI { get; set; }
        public string SearchUserName { get; set; }

    }
}
