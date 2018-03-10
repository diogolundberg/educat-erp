using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class addcolumnhandicapstoenvirollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HaveHandcaps",
                table: "Enrollment",
                newName: "HaveHandicaps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HaveHandicaps",
                table: "Enrollment",
                newName: "HaveHandcaps");
        }
    }
}
