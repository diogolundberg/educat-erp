using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class changed_column_nullable_status_into_document : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentTypeId",
                table: "Documents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentTypeId",
                table: "Documents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
