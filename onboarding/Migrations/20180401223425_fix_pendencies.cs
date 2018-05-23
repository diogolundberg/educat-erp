using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class fix_pendencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pendencies_Enrollments_EnrollmentId",
                table: "Pendencies");

            migrationBuilder.DropIndex(
                name: "IX_Pendencies_EnrollmentId",
                table: "Pendencies");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "Pendencies");

            migrationBuilder.CreateTable(
                name: "EnrollmentPendency",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(nullable: false),
                    PendencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentPendency", x => new { x.EnrollmentId, x.PendencyId });
                    table.ForeignKey(
                        name: "FK_EnrollmentPendency_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentPendency_Pendencies_PendencyId",
                        column: x => x.PendencyId,
                        principalTable: "Pendencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPendency_PendencyId",
                table: "EnrollmentPendency",
                column: "PendencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentPendency");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentId",
                table: "Pendencies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pendencies_EnrollmentId",
                table: "Pendencies",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pendencies_Enrollments_EnrollmentId",
                table: "Pendencies",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
