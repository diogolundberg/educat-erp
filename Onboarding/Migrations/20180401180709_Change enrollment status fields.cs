using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class Changeenrollmentstatusfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "Enrollments",
                newName: "SentAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinanceApproval",
                table: "Enrollments",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcademicApproval",
                table: "Enrollments",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Enrollments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "Enrollments",
                newName: "SendDate");

            migrationBuilder.AlterColumn<bool>(
                name: "FinanceApproval",
                table: "Enrollments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AcademicApproval",
                table: "Enrollments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
