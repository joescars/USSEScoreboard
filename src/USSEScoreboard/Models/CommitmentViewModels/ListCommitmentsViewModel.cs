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

        //public enum CommitmentStatus
        //{
        //    [Description("Not Started")]
        //    NotStarted,
        //    [Description("In Progress")]
        //    InProgress,
        //    [Description("Completed")]
        //    Completed
        //}
        //public class Commitment
        //{
        //    public int Id { get; set; }
        //    public string Title { get; set; }
        //    public string Description { get; set; }
        //    public string Status { get; set; }
        //    public DateTime DateCreated { get; set; }            
        //    //public ApplicationUser User { get; set; }
        //    public string UserId { get; set; }
        //    public string FullName { get; set; }
        //}
    }
}
