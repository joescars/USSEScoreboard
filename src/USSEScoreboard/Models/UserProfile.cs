using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class UserProfile
    {
        public UserProfile()
        {
            this.IsCRM = false;
            this.IsExpenses = false;
            this.IsFRI = false;
            this.TotalAscend = 0;
            this.TotalPresentations = 0;
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }
        public int UserProfileId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        // Collections of Items
        public List<Commitment> Commitments { get; set; }
        public List<ScoreboardItem> ScoreboardItems { get; set; }

        // Many to Many for Wigs
        public List<UserWig> UserWigs { get; set; }

        public bool IsCRM { get; set; }
        public bool IsExpenses { get; set; }
        public bool IsFRI { get; set; }
        public int TotalPresentations { get; set; }
        public int TotalAscend { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
