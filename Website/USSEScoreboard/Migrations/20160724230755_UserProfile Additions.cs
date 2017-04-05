using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace USSEScoreboard.Migrations
{
    public partial class UserProfileAdditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "UserProfile",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "UserProfile",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCRM",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpenses",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFRI",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalAscend",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPresentations",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsCRM",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsExpenses",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsFRI",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "TotalAscend",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "TotalPresentations",
                table: "UserProfile");
        }
    }
}
