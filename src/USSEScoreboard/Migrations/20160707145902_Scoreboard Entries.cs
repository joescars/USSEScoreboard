using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace USSEScoreboard.Migrations
{
    public partial class ScoreboardEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_ScoreboardEntry_ScoreboardItemId",
                table: "ScoreboardEntry",
                column: "ScoreboardItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreboardEntry");
        }
    }
}
