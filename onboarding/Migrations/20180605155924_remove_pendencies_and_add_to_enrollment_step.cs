using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class remove_pendencies_and_add_to_enrollment_step : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pendencies_Enrollments_EnrollmentId",
                table: "Pendencies");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pendencies");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Pendencies",
                newName: "EnrollmentStepId");

            migrationBuilder.RenameIndex(
                name: "IX_Pendencies_EnrollmentId",
                table: "Pendencies",
                newName: "IX_Pendencies_EnrollmentStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pendencies_EnrollmentSteps_EnrollmentStepId",
                table: "Pendencies",
                column: "EnrollmentStepId",
                principalTable: "EnrollmentSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pendencies_EnrollmentSteps_EnrollmentStepId",
                table: "Pendencies");

            migrationBuilder.RenameColumn(
                name: "EnrollmentStepId",
                table: "Pendencies",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Pendencies_EnrollmentStepId",
                table: "Pendencies",
                newName: "IX_Pendencies_EnrollmentId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Pendencies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Pendencies_Enrollments_EnrollmentId",
                table: "Pendencies",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
