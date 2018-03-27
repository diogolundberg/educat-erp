using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class initaldatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressKinds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressKinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcademicApproval = table.Column<bool>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    FinanceApproval = table.Column<bool>(nullable: false),
                    SendDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HighSchoolKinds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighSchoolKinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialNeeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisabilityId = table.Column<int>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialNeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialNeeds_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentTypeId = table.Column<int>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinanceDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnrollmentId = table.Column<int>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceDatas_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressKindId = table.Column<int>(nullable: true),
                    AssumedName = table.Column<string>(nullable: true),
                    BirthCity = table.Column<string>(nullable: true),
                    BirthCountryId = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BirthStateId = table.Column<int>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ComplementAddress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EnrollmentId = table.Column<int>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    GenderId = table.Column<int>(nullable: true),
                    Handicap = table.Column<string>(nullable: true),
                    HighSchoolGraduationCountryId = table.Column<int>(nullable: true),
                    HighSchoolGraduationYear = table.Column<string>(nullable: true),
                    HighSchoolKindId = table.Column<int>(nullable: true),
                    Landline = table.Column<string>(nullable: true),
                    MaritalStatusId = table.Column<int>(nullable: true),
                    MothersName = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    RaceId = table.Column<int>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_AddressKinds_AddressKindId",
                        column: x => x.AddressKindId,
                        principalTable: "AddressKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_Countries_BirthCountryId",
                        column: x => x.BirthCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_States_BirthStateId",
                        column: x => x.BirthStateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_Countries_HighSchoolGraduationCountryId",
                        column: x => x.HighSchoolGraduationCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_HighSchoolKinds_HighSchoolKindId",
                        column: x => x.HighSchoolKindId,
                        principalTable: "HighSchoolKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalDatas_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Guarantors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: true),
                    ComplementAddress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    FinanceDataId = table.Column<int>(nullable: true),
                    Landline = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarantors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guarantors_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guarantors_FinanceDatas_FinanceDataId",
                        column: x => x.FinanceDataId,
                        principalTable: "FinanceDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guarantors_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Representatives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: true),
                    ComplementAddress = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    FinanceDataId = table.Column<int>(nullable: true),
                    Landline = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Representatives_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Representatives_FinanceDatas_FinanceDataId",
                        column: x => x.FinanceDataId,
                        principalTable: "FinanceDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Representatives_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDataDisability",
                columns: table => new
                {
                    PersonalDataId = table.Column<int>(nullable: false),
                    DisabilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDataDisability", x => new { x.PersonalDataId, x.DisabilityId });
                    table.ForeignKey(
                        name: "FK_PersonalDataDisability_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalDataDisability_PersonalDatas_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDataDocument",
                columns: table => new
                {
                    PersonalDataId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDataDocument", x => new { x.PersonalDataId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_PersonalDataDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalDataDocument_PersonalDatas_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDataSpecialNeed",
                columns: table => new
                {
                    PersonalDataId = table.Column<int>(nullable: false),
                    SpecialNeedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDataSpecialNeed", x => new { x.PersonalDataId, x.SpecialNeedId });
                    table.ForeignKey(
                        name: "FK_PersonalDataSpecialNeed_PersonalDatas_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalDataSpecialNeed_SpecialNeeds_SpecialNeedId",
                        column: x => x.SpecialNeedId,
                        principalTable: "SpecialNeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceDatas_EnrollmentId",
                table: "FinanceDatas",
                column: "EnrollmentId",
                unique: true,
                filter: "[EnrollmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Guarantors_CityId",
                table: "Guarantors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Guarantors_FinanceDataId",
                table: "Guarantors",
                column: "FinanceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Guarantors_StateId",
                table: "Guarantors",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDataDisability_DisabilityId",
                table: "PersonalDataDisability",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDataDocument_DocumentId",
                table: "PersonalDataDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_AddressKindId",
                table: "PersonalDatas",
                column: "AddressKindId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_BirthCountryId",
                table: "PersonalDatas",
                column: "BirthCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_BirthStateId",
                table: "PersonalDatas",
                column: "BirthStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_EnrollmentId",
                table: "PersonalDatas",
                column: "EnrollmentId",
                unique: true,
                filter: "[EnrollmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_GenderId",
                table: "PersonalDatas",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_HighSchoolGraduationCountryId",
                table: "PersonalDatas",
                column: "HighSchoolGraduationCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_HighSchoolKindId",
                table: "PersonalDatas",
                column: "HighSchoolKindId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_MaritalStatusId",
                table: "PersonalDatas",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_RaceId",
                table: "PersonalDatas",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDatas_StateId",
                table: "PersonalDatas",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDataSpecialNeed_SpecialNeedId",
                table: "PersonalDataSpecialNeed",
                column: "SpecialNeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Representatives_CityId",
                table: "Representatives",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Representatives_FinanceDataId",
                table: "Representatives",
                column: "FinanceDataId",
                unique: true,
                filter: "[FinanceDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Representatives_StateId",
                table: "Representatives",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialNeeds_DisabilityId",
                table: "SpecialNeeds",
                column: "DisabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guarantors");

            migrationBuilder.DropTable(
                name: "PersonalDataDisability");

            migrationBuilder.DropTable(
                name: "PersonalDataDocument");

            migrationBuilder.DropTable(
                name: "PersonalDataSpecialNeed");

            migrationBuilder.DropTable(
                name: "Representatives");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "PersonalDatas");

            migrationBuilder.DropTable(
                name: "SpecialNeeds");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "FinanceDatas");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "AddressKinds");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "HighSchoolKinds");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Disabilities");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Enrollments");
        }
    }
}
