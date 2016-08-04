using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace USSEScoreboard.Migrations
{
    public partial class CommitmentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Commitment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WigId",
                table: "Commitment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_WigId",
                table: "Commitment",
                column: "WigId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commitment_Wig_WigId",
                table: "Commitment",
                column: "WigId",
                principalTable: "Wig",
                principalColumn: "WigId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitment_Wig_WigId",
                table: "Commitment");

            migrationBuilder.DropIndex(
                name: "IX_Commitment_WigId",
                table: "Commitment");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Commitment");

            migrationBuilder.DropColumn(
                name: "WigId",
                table: "Commitment");
        }
    }
}
