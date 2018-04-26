using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_installment_value_into_plan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Plans",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "InstallmentValue",
                table: "Plans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentValue",
                table: "Plans");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Plans",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
