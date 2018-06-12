using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_enrollment_step_into_finance_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentStepId",
                table: "FinanceDatas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDatas_EnrollmentStepId",
                table: "FinanceDatas",
                column: "EnrollmentStepId",
                unique: true,
                filter: "[EnrollmentStepId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDatas_EnrollmentSteps_EnrollmentStepId",
                table: "FinanceDatas",
                column: "EnrollmentStepId",
                principalTable: "EnrollmentSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDatas_EnrollmentSteps_EnrollmentStepId",
                table: "FinanceDatas");

            migrationBuilder.DropIndex(
                name: "IX_FinanceDatas_EnrollmentStepId",
                table: "FinanceDatas");

            migrationBuilder.DropColumn(
                name: "EnrollmentStepId",
                table: "FinanceDatas");
        }
    }
}
