using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public enum CommitmentStatus
    {
        [Description("Not Started")]
        NotStarted,
        [Description("In Progress")]
        InProgress,
        [Description("Completed")]
        Completed
    }
    public class Commitment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CommitmentStatus Status { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("UserProfileId")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
