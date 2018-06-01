using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class remove_unnecessary_properties_into_enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicApproval",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CourseSummary",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EnrollmentSummary",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "FinanceApproval",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "Enrollments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcademicApproval",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CourseSummary",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentSummary",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinanceApproval",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                table: "Enrollments",
                nullable: true);
        }
    }
}
