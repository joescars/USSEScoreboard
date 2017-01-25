﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using USSEScoreboard.Data;

namespace USSEScoreboard.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161130192302_WIGSetting")]
    partial class WIGSetting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("USSEScoreboard.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("USSEScoreboard.Models.Commitment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<int>("LeadMeasureId");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("LeadMeasureId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Commitment");
                });

            modelBuilder.Entity("USSEScoreboard.Models.GlobalScoreEntry", b =>
                {
                    b.Property<int>("GlobalScoreEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<int>("GlobalScoreType");

                    b.Property<int>("TimeFrameTotal");

                    b.Property<DateTime>("WeekEnding");

                    b.HasKey("GlobalScoreEntryId");

                    b.ToTable("GlobalScoreEntry");
                });

            modelBuilder.Entity("USSEScoreboard.Models.Highlight", b =>
                {
                    b.Property<int>("HighlightId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateStart");

                    b.Property<int>("UserProfileId");

                    b.HasKey("HighlightId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Highlight");
                });

            modelBuilder.Entity("USSEScoreboard.Models.LeadMeasure", b =>
                {
                    b.Property<int>("LeadMeasureId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<int>("WigId");

                    b.HasKey("LeadMeasureId");

                    b.HasIndex("WigId");

                    b.ToTable("LeadMeasure");
                });

            modelBuilder.Entity("USSEScoreboard.Models.ScoreEntry", b =>
                {
                    b.Property<int>("ScoreEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<int>("ScoreType");

                    b.Property<int>("Total");

                    b.Property<int>("UserProfileId");

                    b.Property<DateTime>("WeekEnding");

                    b.HasKey("ScoreEntryId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("ScoreEntry");
                });

            modelBuilder.Entity("USSEScoreboard.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("FirstName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("IsAscendNotes");

                    b.Property<bool>("IsCRM");

                    b.Property<bool>("IsExpenses");

                    b.Property<bool>("IsFRI");

                    b.Property<string>("LastName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("TotalAscend");

                    b.Property<int>("TotalPresentations");

                    b.Property<string>("UserId");

                    b.HasKey("UserProfileId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("USSEScoreboard.Models.UserWig", b =>
                {
                    b.Property<int>("UserProfileId");

                    b.Property<int>("WigId");

                    b.HasKey("UserProfileId", "WigId");

                    b.HasIndex("UserProfileId");

                    b.HasIndex("WigId");

                    b.ToTable("UserWig");
                });

            modelBuilder.Entity("USSEScoreboard.Models.Wig", b =>
                {
                    b.Property<int>("WigId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("WigId");

                    b.ToTable("Wig");
                });

            modelBuilder.Entity("USSEScoreboard.Models.WIGSetting", b =>
                {
                    b.Property<int>("WIGSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AscendWinGoal");

                    b.Property<double>("CommunityWinGoal");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("WIGSettingId");

                    b.ToTable("WigSetting");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("USSEScoreboard.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("USSEScoreboard.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("USSEScoreboard.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("USSEScoreboard.Models.Commitment", b =>
                {
                    b.HasOne("USSEScoreboard.Models.LeadMeasure", "LeadMeasure")
                        .WithMany()
                        .HasForeignKey("LeadMeasureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("USSEScoreboard.Models.UserProfile", "UserProfile")
                        .WithMany("Commitments")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("USSEScoreboard.Models.Highlight", b =>
                {
                    b.HasOne("USSEScoreboard.Models.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("USSEScoreboard.Models.LeadMeasure", b =>
                {
                    b.HasOne("USSEScoreboard.Models.Wig", "Wig")
                        .WithMany()
                        .HasForeignKey("WigId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("USSEScoreboard.Models.ScoreEntry", b =>
                {
                    b.HasOne("USSEScoreboard.Models.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("USSEScoreboard.Models.UserProfile", b =>
                {
                    b.HasOne("USSEScoreboard.Models.ApplicationUser", "User")
                        .WithMany("UserProfile")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("USSEScoreboard.Models.UserWig", b =>
                {
                    b.HasOne("USSEScoreboard.Models.UserProfile", "UserProfile")
                        .WithMany("UserWigs")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("USSEScoreboard.Models.Wig", "Wig")
                        .WithMany("UserWigs")
                        .HasForeignKey("WigId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
