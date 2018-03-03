using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.Migrations
{
    public partial class AddSeedClientGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ClientGroups (Name, Description) VALUES('Pakket', 'Pakket omgeving')");
            migrationBuilder.Sql("INSERT INTO ClientGroups (Name, Description) VALUES('Rabo', 'Rabo omgeving')");
            migrationBuilder.Sql("INSERT INTO ClientGroups (Name, Description) VALUES('ING', 'ING omgeving')");
            migrationBuilder.Sql("INSERT INTO ClientGroups (Name, Description) VALUES('Syntus', 'Syntus omgeving')");
            migrationBuilder.Sql("INSERT INTO ClientGroups (Name, Description) VALUES('Aegon', 'Aegon omgeving')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ClientGroups WHERE Name IN ('Pakket', 'Rabo', 'ING', 'Syntus', 'Aegon')");
        }
    }
}
