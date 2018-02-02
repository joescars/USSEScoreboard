using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboard.Website.Models
{
    public enum CommitmentStatus
    {
        [Description("Not Started")]
        Inactive,
        [Description("In Progress")]
        Active,
        [Description("Completed")]
        Complete,
        [Description("Archive")]
        Archive
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
        public DateTime DateModified { get; set; }

        [ForeignKey("LeadMeasureId")]
        public int LeadMeasureId { get; set; }
        public LeadMeasure LeadMeasure { get; set; }

        public Commitment()
        {
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }
    }
}
