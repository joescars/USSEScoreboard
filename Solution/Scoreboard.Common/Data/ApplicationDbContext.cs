using Microsoft.EntityFrameworkCore;
using Scoreboard.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Scoreboard.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(string ConnStr)
            : base (new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(ConnStr).Options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Highlight> Highlight { get; set; }
    }
}
