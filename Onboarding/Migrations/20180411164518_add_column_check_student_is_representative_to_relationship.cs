using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class add_column_check_student_is_representative_to_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckStudentIsRepresentative",
                table: "Relationships",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckStudentIsRepresentative",
                table: "Relationships");
        }
    }
}
