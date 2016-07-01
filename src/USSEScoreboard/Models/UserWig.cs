using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class UserWig
    {
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int WigId { get; set; }
        public Wig Wig { get; set; }
    }
}
