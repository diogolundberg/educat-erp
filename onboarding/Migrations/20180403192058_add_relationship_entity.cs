using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace onboarding.Migrations
{
    public partial class add_relationship_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "Representatives");

            migrationBuilder.AddColumn<int>(
                name: "RelationshipId",
                table: "Representatives",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Representatives_RelationshipId",
                table: "Representatives",
                column: "RelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Representatives_Relationships_RelationshipId",
                table: "Representatives",
                column: "RelationshipId",
                principalTable: "Relationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Representatives_Relationships_RelationshipId",
                table: "Representatives");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropIndex(
                name: "IX_Representatives_RelationshipId",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "RelationshipId",
                table: "Representatives");

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                table: "Representatives",
                nullable: true);
        }
    }
}
