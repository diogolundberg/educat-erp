using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class add_columns_birth_date_name_and_birth_city_name_to_personal_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthCityName",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthStateName",
                table: "PersonalDatas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthCityName",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "BirthStateName",
                table: "PersonalDatas");
        }
    }
}
