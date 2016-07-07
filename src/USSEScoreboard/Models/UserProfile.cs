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
            //this.Wigs = new HashSet<Wig>();
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
    }
}
