using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace USSEScoreboard.Migrations
{
    public partial class CommitmentLead1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitment_Wig_WigId",
                table: "Commitment");

            migrationBuilder.DropIndex(
                name: "IX_Commitment_WigId",
                table: "Commitment");

            migrationBuilder.DropColumn(
                name: "WigId",
                table: "Commitment");

            migrationBuilder.AddColumn<int>(
                name: "LeadMeasureId",
                table: "Commitment",
                nullable: true);

            // Custom being we are adding a new foriegn key. 
            // Work around
            migrationBuilder.Sql(@"UPDATE dbo.Commitment Set LeadMeasureId = 1
                        where LeadMeasureId IS NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadMeasureId",
                table: "Commitment");

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
    }
}
