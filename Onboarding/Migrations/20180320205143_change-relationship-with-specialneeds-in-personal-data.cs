using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class changerelationshipwithspecialneedsinpersonaldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonalDataId",
                table: "SpecialNeeds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialNeeds_PersonalDataId",
                table: "SpecialNeeds",
                column: "PersonalDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialNeeds_PersonalData_PersonalDataId",
                table: "SpecialNeeds",
                column: "PersonalDataId",
                principalTable: "PersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialNeeds_PersonalData_PersonalDataId",
                table: "SpecialNeeds");

            migrationBuilder.DropIndex(
                name: "IX_SpecialNeeds_PersonalDataId",
                table: "SpecialNeeds");

            migrationBuilder.DropColumn(
                name: "PersonalDataId",
                table: "SpecialNeeds");
        }
    }
}
