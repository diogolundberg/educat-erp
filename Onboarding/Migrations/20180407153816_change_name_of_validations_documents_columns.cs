using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class change_name_of_validations_documents_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSpouse",
                table: "Relationships",
                newName: "CheckSpouse");

            migrationBuilder.RenameColumn(
                name: "IsForeign",
                table: "Nationalities",
                newName: "CheckForeign");

            migrationBuilder.AddColumn<bool>(
                name: "CheckForeignGraduation",
                table: "Countries",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckForeignGraduation",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "CheckSpouse",
                table: "Relationships",
                newName: "IsSpouse");

            migrationBuilder.RenameColumn(
                name: "CheckForeign",
                table: "Nationalities",
                newName: "IsForeign");
        }
    }
}
