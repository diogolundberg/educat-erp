using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Onboarding.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressKinds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    SendBy = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StateId = table.Column<Guid>(nullable: false)
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
                name: "PersonalData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AddressKindId = table.Column<Guid>(nullable: true),
                    AssumedName = table.Column<string>(nullable: true),
                    BirthCity = table.Column<string>(nullable: true),
                    BirthCountryId = table.Column<Guid>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BirthStateId = table.Column<Guid>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ComplementAddress = table.Column<string>(nullable: true),
                    DbState = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    GenderId = table.Column<Guid>(nullable: true),
                    Handicap = table.Column<string>(nullable: true),
                    HighSchoolGraduationCountryId = table.Column<Guid>(nullable: true),
                    HighSchoolGraduationYear = table.Column<string>(nullable: true),
                    HighSchoolKindId = table.Column<Guid>(nullable: true),
                    Landline = table.Column<string>(nullable: true),
                    MaritalStatusId = table.Column<Guid>(nullable: true),
                    MothersName = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    RaceId = table.Column<Guid>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    StateId = table.Column<Guid>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalData_AddressKinds_AddressKindId",
                        column: x => x.AddressKindId,
                        principalTable: "AddressKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Countries_BirthCountryId",
                        column: x => x.BirthCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_States_BirthStateId",
                        column: x => x.BirthStateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalData_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Countries_HighSchoolGraduationCountryId",
                        column: x => x.HighSchoolGraduationCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_HighSchoolKinds_HighSchoolKindId",
                        column: x => x.HighSchoolKindId,
                        principalTable: "HighSchoolKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PersonalDataId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disabilities_PersonalData_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialNeeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    DbState = table.Column<string>(nullable: true),
                    DisabilityId = table.Column<Guid>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Disabilities_PersonalDataId",
                table: "Disabilities",
                column: "PersonalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_AddressKindId",
                table: "PersonalData",
                column: "AddressKindId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_BirthCountryId",
                table: "PersonalData",
                column: "BirthCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_BirthStateId",
                table: "PersonalData",
                column: "BirthStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_EnrollmentId",
                table: "PersonalData",
                column: "EnrollmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_GenderId",
                table: "PersonalData",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_HighSchoolGraduationCountryId",
                table: "PersonalData",
                column: "HighSchoolGraduationCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_HighSchoolKindId",
                table: "PersonalData",
                column: "HighSchoolKindId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_MaritalStatusId",
                table: "PersonalData",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_RaceId",
                table: "PersonalData",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_StateId",
                table: "PersonalData",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialNeeds_DisabilityId",
                table: "SpecialNeeds",
                column: "DisabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "SpecialNeeds");

            migrationBuilder.DropTable(
                name: "Disabilities");

            migrationBuilder.DropTable(
                name: "PersonalData");

            migrationBuilder.DropTable(
                name: "AddressKinds");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "HighSchoolKinds");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "Races");
        }
    }
}
