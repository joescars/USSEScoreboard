using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class ScoreboardEntry
    {
        // tracks the actuall entries for score totals

        public int ScoreboardEntryId { get; set; }
        [ForeignKey("ScoreboardItemId")]
        public int ScoreboardItemId { get; set; }
        public ScoreboardItem ScoreboardItem { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
