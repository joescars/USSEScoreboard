using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class ScoreboardItem
    {

        // Used for tracking individual score board items against users. 

        public int ScoreboardItemId { get; set; }
        [ForeignKey("UserProfileId")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
