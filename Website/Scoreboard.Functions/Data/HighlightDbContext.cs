using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scoreboard.Functions.Models;

namespace Scoreboard.Functions.Data
{
    public class HighlightDbContext : DbContext
    {
        public HighlightDbContext()
            : base("name=HighlightContext")
        {
        }

        public virtual DbSet<Highlight> Highlights { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
    }
}
