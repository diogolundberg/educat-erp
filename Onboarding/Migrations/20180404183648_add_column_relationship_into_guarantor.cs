using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class add_column_relationship_into_guarantor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelationshipId",
                table: "Guarantors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guarantors_RelationshipId",
                table: "Guarantors",
                column: "RelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guarantors_Relationships_RelationshipId",
                table: "Guarantors",
                column: "RelationshipId",
                principalTable: "Relationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guarantors_Relationships_RelationshipId",
                table: "Guarantors");

            migrationBuilder.DropIndex(
                name: "IX_Guarantors_RelationshipId",
                table: "Guarantors");

            migrationBuilder.DropColumn(
                name: "RelationshipId",
                table: "Guarantors");
        }
    }
}
