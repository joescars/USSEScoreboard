using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Functions.Models
{
    [Table("Highlight")]
    public class Highlight
    {
        public int HighlightId { get; set; }
        public string Body { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        //public Highlight()
        //{
        //    this.DateCreated = DateTime.Now;
        //    this.DateModified = DateTime.Now;
        //}
    }
}
