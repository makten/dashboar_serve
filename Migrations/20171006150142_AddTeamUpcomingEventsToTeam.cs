using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.Migrations
{
    public partial class AddTeamUpcomingEventsToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "UpcomingEvents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UpcomingEvents_TeamId",
                table: "UpcomingEvents",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_UpcomingEvents_Teams_TeamId",
                table: "UpcomingEvents",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpcomingEvents_Teams_TeamId",
                table: "UpcomingEvents");

            migrationBuilder.DropIndex(
                name: "IX_UpcomingEvents_TeamId",
                table: "UpcomingEvents");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "UpcomingEvents");
        }
    }
}
