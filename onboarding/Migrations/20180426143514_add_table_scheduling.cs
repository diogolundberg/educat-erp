using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_table_scheduling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedulings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Breaks = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    EndAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    Intervals = table.Column<string>(nullable: true),
                    OnboardingId = table.Column<int>(nullable: false),
                    StartAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedulings_Onboardings_OnboardingId",
                        column: x => x.OnboardingId,
                        principalTable: "Onboardings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_OnboardingId",
                table: "Schedulings",
                column: "OnboardingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedulings");
        }
    }
}
