using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_columns_schedule_time_for_scheduling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScheduleEndTime",
                table: "Schedulings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScheduleStartTime",
                table: "Schedulings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleEndTime",
                table: "Schedulings");

            migrationBuilder.DropColumn(
                name: "ScheduleStartTime",
                table: "Schedulings");
        }
    }
}
