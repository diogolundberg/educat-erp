using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_finance_section_and_academic_section : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Sections",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Sections");
        }
    }
}
