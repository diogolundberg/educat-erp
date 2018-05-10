using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace finance.Migrations
{
    public partial class add_billet_to_invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Billet",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Billet",
                table: "Invoices");
        }
    }
}
