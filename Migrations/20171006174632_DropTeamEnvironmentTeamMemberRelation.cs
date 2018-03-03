using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.Migrations
{
    public partial class DropTeamEnvironmentTeamMemberRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamEnvironments_TeamMembers_TeamMemberId",
                table: "TeamEnvironments");

            migrationBuilder.DropIndex(
                name: "IX_TeamEnvironments_TeamMemberId",
                table: "TeamEnvironments");

            migrationBuilder.DropColumn(
                name: "TeamMemberId",
                table: "TeamEnvironments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamMemberId",
                table: "TeamEnvironments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamEnvironments_TeamMemberId",
                table: "TeamEnvironments",
                column: "TeamMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEnvironments_TeamMembers_TeamMemberId",
                table: "TeamEnvironments",
                column: "TeamMemberId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
