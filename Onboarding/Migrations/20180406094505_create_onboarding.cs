using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class create_onboarding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OnboardingId",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Onboardings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    EndAt = table.Column<DateTime>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    StartAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboardings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_OnboardingId",
                table: "Enrollments",
                column: "OnboardingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Onboardings_OnboardingId",
                table: "Enrollments",
                column: "OnboardingId",
                principalTable: "Onboardings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Onboardings_OnboardingId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "Onboardings");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_OnboardingId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "OnboardingId",
                table: "Enrollments");
        }
    }
}
