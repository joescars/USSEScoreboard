using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class LeadMeasure
    {
        public int LeadMeasureId { get; set; }
        public string Description { get; set; }
        [ForeignKey("WigId")]
        public int WigId { get; set; }
        public Wig Wig { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
