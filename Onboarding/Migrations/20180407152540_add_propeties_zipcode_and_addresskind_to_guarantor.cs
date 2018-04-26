using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_propeties_zipcode_and_addresskind_to_guarantor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressKindId",
                table: "Guarantors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                table: "Guarantors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guarantors_AddressKindId",
                table: "Guarantors",
                column: "AddressKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guarantors_AddressKinds_AddressKindId",
                table: "Guarantors",
                column: "AddressKindId",
                principalTable: "AddressKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guarantors_AddressKinds_AddressKindId",
                table: "Guarantors");

            migrationBuilder.DropIndex(
                name: "IX_Guarantors_AddressKindId",
                table: "Guarantors");

            migrationBuilder.DropColumn(
                name: "AddressKindId",
                table: "Guarantors");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Guarantors");
        }
    }
}
