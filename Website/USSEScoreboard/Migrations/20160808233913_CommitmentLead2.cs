using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace USSEScoreboard.Migrations
{
    public partial class CommitmentLead2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LeadMeasureId",
                table: "Commitment",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_LeadMeasureId",
                table: "Commitment",
                column: "LeadMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commitment_LeadMeasure_LeadMeasureId",
                table: "Commitment",
                column: "LeadMeasureId",
                principalTable: "LeadMeasure",
                principalColumn: "LeadMeasureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitment_LeadMeasure_LeadMeasureId",
                table: "Commitment");

            migrationBuilder.DropIndex(
                name: "IX_Commitment_LeadMeasureId",
                table: "Commitment");

            migrationBuilder.AlterColumn<int>(
                name: "LeadMeasureId",
                table: "Commitment",
                nullable: true);
        }
    }
}
