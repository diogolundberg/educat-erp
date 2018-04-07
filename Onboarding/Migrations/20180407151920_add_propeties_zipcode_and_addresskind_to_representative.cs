using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class add_propeties_zipcode_and_addresskind_to_representative : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressKindId",
                table: "Representatives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                table: "Representatives",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Representatives_AddressKindId",
                table: "Representatives",
                column: "AddressKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_Representatives_AddressKinds_AddressKindId",
                table: "Representatives",
                column: "AddressKindId",
                principalTable: "AddressKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Representatives_AddressKinds_AddressKindId",
                table: "Representatives");

            migrationBuilder.DropIndex(
                name: "IX_Representatives_AddressKindId",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "AddressKindId",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Representatives");
        }
    }
}
