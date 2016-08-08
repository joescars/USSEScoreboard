using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using USSEScoreboard.Models;

namespace USSEScoreboard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Create the Many-To-Many relationship for Wigs
            builder.Entity<UserWig>()
                .HasKey(w => new { w.UserProfileId, w.WigId });

            builder.Entity<UserWig>()
                .HasOne(uw => uw.UserProfile)
                .WithMany(u => u.UserWigs)
                .HasForeignKey(uw => uw.UserProfileId);

            builder.Entity<UserWig>()
                .HasOne(uw => uw.Wig)
                .WithMany(u => u.UserWigs)
                .HasForeignKey(uw => uw.WigId);

        }

        public DbSet<Commitment> Commitment { get; set; }

        public DbSet<Wig> Wig { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }
        
        // Individual Based Tracking. Not used currently
        public DbSet<ScoreEntry> ScoreEntry { get; set; }

        // Global Based Tracking. Currently in use.
        public DbSet<GlobalScoreEntry> GlobalScoreEntry { get; set; }

        public DbSet<LeadMeasure> LeadMeasure { get; set; }

    }
}
