using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class add_relationship_with_personaldata_and_city : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthCity",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "City",
                table: "PersonalDatas");

            migrationBuilder.AddColumn<int>(
                name: "BirthCityId",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_BirthCityId",
                table: "PersonalDatas",
                column: "BirthCityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_CityId",
                table: "PersonalDatas",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDatas_Cities_BirthCityId",
                table: "PersonalDatas",
                column: "BirthCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDatas_Cities_CityId",
                table: "PersonalDatas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDatas_Cities_BirthCityId",
                table: "PersonalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDatas_Cities_CityId",
                table: "PersonalDatas");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDatas_BirthCityId",
                table: "PersonalDatas");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDatas_CityId",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "BirthCityId",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "PersonalDatas");

            migrationBuilder.AddColumn<string>(
                name: "BirthCity",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PersonalDatas",
                nullable: true);
        }
    }
}
