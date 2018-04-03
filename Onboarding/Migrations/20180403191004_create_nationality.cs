using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class create_nationality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "PersonalDatas");

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "PersonalDatas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_NationalityId",
                table: "PersonalDatas",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDatas_Nationalities_NationalityId",
                table: "PersonalDatas",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDatas_Nationalities_NationalityId",
                table: "PersonalDatas");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDatas_NationalityId",
                table: "PersonalDatas");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "PersonalDatas");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "PersonalDatas",
                nullable: true);
        }
    }
}
