using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class create_plan_and_paymentmethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "FinanceDatas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "FinanceDatas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
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
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    Installments = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDatas_PaymentMethodId",
                table: "FinanceDatas",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDatas_PlanId",
                table: "FinanceDatas",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDatas_PaymentMethod_PaymentMethodId",
                table: "FinanceDatas",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceDatas_Plans_PlanId",
                table: "FinanceDatas",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDatas_PaymentMethod_PaymentMethodId",
                table: "FinanceDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceDatas_Plans_PlanId",
                table: "FinanceDatas");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_FinanceDatas_PaymentMethodId",
                table: "FinanceDatas");

            migrationBuilder.DropIndex(
                name: "IX_FinanceDatas_PlanId",
                table: "FinanceDatas");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "FinanceDatas");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "FinanceDatas");
        }
    }
}
