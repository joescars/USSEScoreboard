using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace USSEScoreboard.Migrations
{
    public partial class AscendNotesColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAscendNotes",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAscendNotes",
                table: "UserProfile");
        }
    }
}
