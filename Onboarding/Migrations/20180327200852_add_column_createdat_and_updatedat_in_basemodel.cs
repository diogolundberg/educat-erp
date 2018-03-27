using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class add_column_createdat_and_updatedat_in_basemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "States",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "States",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SpecialNeeds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SpecialNeeds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Representatives",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Representatives",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Races",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Races",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MaritalStatuses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MaritalStatuses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HighSchoolKinds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "HighSchoolKinds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Guarantors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Guarantors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Genders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Genders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FinanceDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FinanceDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DocumentTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DocumentTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Disabilities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Disabilities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AddressKinds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AddressKinds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "States");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SpecialNeeds");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SpecialNeeds");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MaritalStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HighSchoolKinds");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "HighSchoolKinds");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Guarantors");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Guarantors");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FinanceDatas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FinanceDatas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Disabilities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Disabilities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AddressKinds");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AddressKinds");
        }
    }
}
