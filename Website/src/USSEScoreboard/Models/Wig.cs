using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public enum WigStatus
    {
        [Description("Not Started")]
        NotStarted,
        [Description("In Progress")]
        InProgress,
        [Description("Completed")]
        Completed
    }
    public class Wig
    {
        public int WigId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WigStatus Status { get; set; }
        public DateTime DateCreated { get; set; }

        // Many to Many for Wigs
        public List<UserWig> UserWigs { get; set; }
    }
}
