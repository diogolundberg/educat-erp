using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class add_column_deadline_in_enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Documents");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Enrollments");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Documents",
                nullable: true);
        }
    }
}
