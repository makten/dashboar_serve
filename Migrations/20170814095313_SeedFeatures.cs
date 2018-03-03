using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Hatchback')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Air-condition')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('ABS')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('5-Doors')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Navigation')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('Hatchback', 'Air-condition', 'ABS', '5-Doors', 'Navigation')");
        }
    }
}
