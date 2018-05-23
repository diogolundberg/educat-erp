using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_column_sentat_to_enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "Enrollments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Enrollments");
        }
    }
}
