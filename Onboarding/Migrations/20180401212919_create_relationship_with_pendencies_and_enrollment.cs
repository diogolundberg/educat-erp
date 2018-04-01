using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class create_relationship_with_pendencies_and_enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentId",
                table: "Pendencies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pendencies_EnrollmentId",
                table: "Pendencies",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pendencies_Enrollments_EnrollmentId",
                table: "Pendencies",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pendencies_Enrollments_EnrollmentId",
                table: "Pendencies");

            migrationBuilder.DropIndex(
                name: "IX_Pendencies_EnrollmentId",
                table: "Pendencies");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "Pendencies");
        }
    }
}
