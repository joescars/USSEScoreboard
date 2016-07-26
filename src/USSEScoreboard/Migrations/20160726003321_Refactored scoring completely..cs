using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace USSEScoreboard.Migrations
{
    public partial class Refactoredscoringcompletely : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PresentationEntry");

            migrationBuilder.DropTable(
                name: "ScoreboardEntry");

            migrationBuilder.DropTable(
                name: "ScoreboardItem");

            migrationBuilder.CreateTable(
                name: "ScoreEntry",
                columns: table => new
                {
                    ScoreEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ScoreType = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false),
                    WeekEnding = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreEntry", x => x.ScoreEntryId);
                    table.ForeignKey(
                        name: "FK_ScoreEntry_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoreEntry_UserProfileId",
                table: "ScoreEntry",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreEntry");

            migrationBuilder.CreateTable(
                name: "PresentationEntry",
                columns: table => new
                {
                    PresentationEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false),
                    WeekEnding = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresentationEntry", x => x.PresentationEntryId);
                    table.ForeignKey(
                        name: "FK_PresentationEntry_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoreboardItem",
                columns: table => new
                {
                    ScoreboardItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Total = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreboardItem", x => x.ScoreboardItemId);
                    table.ForeignKey(
                        name: "FK_ScoreboardItem_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoreboardEntry",
                columns: table => new
                {
                    ScoreboardEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ScoreboardItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreboardEntry", x => x.ScoreboardEntryId);
                    table.ForeignKey(
                        name: "FK_ScoreboardEntry_ScoreboardItem_ScoreboardItemId",
                        column: x => x.ScoreboardItemId,
                        principalTable: "ScoreboardItem",
                        principalColumn: "ScoreboardItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PresentationEntry_UserProfileId",
                table: "PresentationEntry",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreboardEntry_ScoreboardItemId",
                table: "ScoreboardEntry",
                column: "ScoreboardItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreboardItem_UserProfileId",
                table: "ScoreboardItem",
                column: "UserProfileId");
        }
    }
}
