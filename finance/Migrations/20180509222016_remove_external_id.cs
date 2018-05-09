using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace finance.Migrations
{
    public partial class remove_external_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "InvoiceItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "InvoiceItems",
                nullable: true);
        }
    }
}
