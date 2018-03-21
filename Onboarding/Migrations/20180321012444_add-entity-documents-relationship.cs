using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class addentitydocumentsrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_PersonalData_PersonalDataId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_PersonalDataId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "PersonalDataId",
                table: "Documents");

            migrationBuilder.CreateTable(
                name: "PersonalDataDocument",
                columns: table => new
                {
                    PersonalDataId = table.Column<Guid>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDataDocument", x => new { x.PersonalDataId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_PersonalDataDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalDataDocument_PersonalData_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDataDocument_DocumentId",
                table: "PersonalDataDocument",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalDataDocument");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalDataId",
                table: "Documents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonalDataId",
                table: "Documents",
                column: "PersonalDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_PersonalData_PersonalDataId",
                table: "Documents",
                column: "PersonalDataId",
                principalTable: "PersonalData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
