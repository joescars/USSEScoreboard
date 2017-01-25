using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class PresentationEntry
    {
        public int PresentationEntryId { get; set; }
        [ForeignKey("UserProfileId")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int Total { get; set; }
        public DateTime WeekEnding { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public PresentationEntry()
        {
            this.Total = 0;
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }
    }
}
