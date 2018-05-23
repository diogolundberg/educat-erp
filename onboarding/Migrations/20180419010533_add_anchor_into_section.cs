using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_anchor_into_section : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Pendencies");

            migrationBuilder.AddColumn<string>(
                name: "Anchor",
                table: "Sections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anchor",
                table: "Sections");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Pendencies",
                nullable: true);
        }
    }
}
