using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_enrollment_step_into_contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentStepId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EnrollmentStepId",
                table: "Contracts",
                column: "EnrollmentStepId",
                unique: true,
                filter: "[EnrollmentStepId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_EnrollmentSteps_EnrollmentStepId",
                table: "Contracts",
                column: "EnrollmentStepId",
                principalTable: "EnrollmentSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_EnrollmentSteps_EnrollmentStepId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_EnrollmentStepId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EnrollmentStepId",
                table: "Contracts");
        }
    }
}
