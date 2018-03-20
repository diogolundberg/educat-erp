using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class changerelationshipntonwithdisabilitiesandspecialneedsinpersonaldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disabilities_PersonalData_PersonalDataId",
                table: "Disabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialNeeds_PersonalData_PersonalDataId",
                table: "SpecialNeeds");

            migrationBuilder.DropIndex(
                name: "IX_SpecialNeeds_PersonalDataId",
                table: "SpecialNeeds");

            migrationBuilder.DropIndex(
                name: "IX_Disabilities_PersonalDataId",
                table: "Disabilities");

            migrationBuilder.DropColumn(
                name: "PersonalDataId",
                table: "SpecialNeeds");

            migrationBuilder.DropColumn(
                name: "PersonalDataId",
                table: "Disabilities");

            migrationBuilder.CreateTable(
                name: "PersonalDataDisability",
                columns: table => new
                {
                    PersonalDataId = table.Column<Guid>(nullable: false),
                    DisabilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDataDisability", x => new { x.PersonalDataId, x.DisabilityId });
                    table.ForeignKey(
                        name: "FK_PersonalDataDisability_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalDataDisability_PersonalData_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDataSpecialNeed",
                columns: table => new
                {
                    PersonalDataId = table.Column<Guid>(nullable: false),
                    SpecialNeedId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDataSpecialNeed", x => new { x.PersonalDataId, x.SpecialNeedId });
                    table.ForeignKey(
                        name: "FK_PersonalDataSpecialNeed_PersonalData_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalDataSpecialNeed_SpecialNeeds_SpecialNeedId",
                        column: x => x.SpecialNeedId,
                        principalTable: "SpecialNeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDataDisability_DisabilityId",
                table: "PersonalDataDisability",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDataSpecialNeed_SpecialNeedId",
                table: "PersonalDataSpecialNeed",
                column: "SpecialNeedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalDataDisability");

            migrationBuilder.DropTable(
                name: "PersonalDataSpecialNeed");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalDataId",
                table: "SpecialNeeds",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalDataId",
                table: "Disabilities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialNeeds_PersonalDataId",
                table: "SpecialNeeds",
                column: "PersonalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Disabilities_PersonalDataId",
                table: "Disabilities",
                column: "PersonalDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disabilities_PersonalData_PersonalDataId",
                table: "Disabilities",
                column: "PersonalDataId",
                principalTable: "PersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialNeeds_PersonalData_PersonalDataId",
                table: "SpecialNeeds",
                column: "PersonalDataId",
                principalTable: "PersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
