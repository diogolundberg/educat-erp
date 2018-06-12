using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_enrollment_step_into_personal_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentStepId",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_EnrollmentStepId",
                table: "PersonalDatas",
                column: "EnrollmentStepId",
                unique: true,
                filter: "[EnrollmentStepId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDatas_EnrollmentSteps_EnrollmentStepId",
                table: "PersonalDatas",
                column: "EnrollmentStepId",
                principalTable: "EnrollmentSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDatas_EnrollmentSteps_EnrollmentStepId",
                table: "PersonalDatas");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDatas_EnrollmentStepId",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "EnrollmentStepId",
                table: "PersonalDatas");
        }
    }
}
