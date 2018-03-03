using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.Migrations
{
    public partial class AddTeamEnvironmentClientGroupRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientGroupId",
                table: "TeamEnvironments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamEnvironments_ClientGroupId",
                table: "TeamEnvironments",
                column: "ClientGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEnvironments_ClientGroups_ClientGroupId",
                table: "TeamEnvironments",
                column: "ClientGroupId",
                principalTable: "ClientGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamEnvironments_ClientGroups_ClientGroupId",
                table: "TeamEnvironments");

            migrationBuilder.DropIndex(
                name: "IX_TeamEnvironments_ClientGroupId",
                table: "TeamEnvironments");

            migrationBuilder.DropColumn(
                name: "ClientGroupId",
                table: "TeamEnvironments");
        }
    }
}
