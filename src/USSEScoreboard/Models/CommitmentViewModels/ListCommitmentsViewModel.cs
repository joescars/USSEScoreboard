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

    }
}
