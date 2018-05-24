using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_column_to_enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Schedulings");

            migrationBuilder.RenameColumn(
                name: "EnrollmentInfo",
                table: "Enrollments",
                newName: "EnrollmentSummary");

            migrationBuilder.AddColumn<DateTime>(
                name: "CourseSummary",
                table: "Enrollments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseSummary",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "EnrollmentSummary",
                table: "Enrollments",
                newName: "EnrollmentInfo");

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
                    ScheduleEndTime = table.Column<string>(nullable: true),
                    ScheduleStartTime = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    EnrollmentId = table.Column<int>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    Hour = table.Column<string>(nullable: true),
                    SchedulingId = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Schedulings_SchedulingId",
                        column: x => x.SchedulingId,
                        principalTable: "Schedulings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EnrollmentId",
                table: "Appointments",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SchedulingId",
                table: "Appointments",
                column: "SchedulingId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_OnboardingId",
                table: "Schedulings",
                column: "OnboardingId");
        }
    }
}
