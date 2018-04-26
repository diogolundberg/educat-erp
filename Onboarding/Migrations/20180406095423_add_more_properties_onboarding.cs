using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_more_properties_onboarding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Onboardings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Onboardings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Onboardings");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Onboardings");
        }
    }
}
