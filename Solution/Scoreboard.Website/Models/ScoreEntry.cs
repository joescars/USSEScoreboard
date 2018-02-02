using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scoreboard.Website.Models
{
    public enum ScoreEntryType
    {
        [Description("Community Presentation")]
        Presentation,
        [Description("Ascend in Progress")]
        Ascend
    }
    public class ScoreEntry
    {
        public int ScoreEntryId { get; set; }
        [ForeignKey("UserProfileId")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int Total { get; set; }
        public ScoreEntryType ScoreType { get; set; }
        public DateTime WeekEnding { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public ScoreEntry()
        {
            this.Total = 0;
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }
    }
}
