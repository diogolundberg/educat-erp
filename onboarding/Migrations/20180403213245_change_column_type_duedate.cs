using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class change_column_type_duedate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Plans",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Plans",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
