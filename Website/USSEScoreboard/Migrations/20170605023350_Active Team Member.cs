using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace USSEScoreboard.Migrations
{
    public partial class ActiveTeamMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserWig_UserProfileId",
                table: "UserWig");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<bool>(
                name: "IsActiveTeamMember",
                table: "UserProfile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsActiveTeamMember",
                table: "UserProfile");

            migrationBuilder.CreateIndex(
                name: "IX_UserWig_UserProfileId",
                table: "UserWig",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
