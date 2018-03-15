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
                name: "AddressTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CivilStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivilStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    AddressTypeId = table.Column<Guid>(nullable: true),
                    BirthCity = table.Column<string>(nullable: true),
                    BirthStateId = table.Column<Guid>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Cep = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CivilStatusId = table.Column<Guid>(nullable: true),
                    CommitedBy = table.Column<string>(nullable: true),
                    CommittedAt = table.Column<DateTime>(nullable: false),
                    CountryOfGraduationFromHighSchoolId = table.Column<Guid>(nullable: true),
                    CountryStateId = table.Column<Guid>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false),
                    GenderId = table.Column<Guid>(nullable: true),
                    GuarantorAddress = table.Column<string>(nullable: true),
                    GuarantorCnpj = table.Column<string>(nullable: true),
                    GuarantorContact = table.Column<string>(nullable: true),
                    GuarantorCpf = table.Column<string>(nullable: true),
                    GuarantorDocumentTypeId = table.Column<Guid>(nullable: true),
                    GuarantorEmail = table.Column<string>(nullable: true),
                    GuarantorName = table.Column<string>(nullable: true),
                    GuarantorPhone1 = table.Column<string>(nullable: true),
                    GuarantorPhone2 = table.Column<string>(nullable: true),
                    HasHandicaps = table.Column<bool>(nullable: true),
                    MothersName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NationalityId = table.Column<Guid>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    OriginCountryId = table.Column<Guid>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneTypeId = table.Column<Guid>(nullable: true),
                    RaceId = table.Column<Guid>(nullable: true),
                    ResponsibleAddress = table.Column<string>(nullable: true),
                    ResponsibleCnpj = table.Column<string>(nullable: true),
                    ResponsibleContact = table.Column<string>(nullable: true),
                    ResponsibleCpf = table.Column<string>(nullable: true),
                    ResponsibleDocumentTypeId = table.Column<Guid>(nullable: true),
                    ResponsibleEmail = table.Column<string>(nullable: true),
                    ResponsibleName = table.Column<string>(nullable: true),
                    ResponsiblePhone1 = table.Column<string>(nullable: true),
                    ResponsiblePhone2 = table.Column<string>(nullable: true),
                    SchoolId = table.Column<Guid>(nullable: true),
                    SendBy = table.Column<DateTimeOffset>(nullable: true),
                    SocialName = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    YearofHighSchoolGraduation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_AddressTypes_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_States_BirthStateId",
                        column: x => x.BirthStateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_CivilStatus_CivilStatusId",
                        column: x => x.CivilStatusId,
                        principalTable: "CivilStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Countries_CountryOfGraduationFromHighSchoolId",
                        column: x => x.CountryOfGraduationFromHighSchoolId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_States_CountryStateId",
                        column: x => x.CountryStateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_DocumentTypes_GuarantorDocumentTypeId",
                        column: x => x.GuarantorDocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Countries_OriginCountryId",
                        column: x => x.OriginCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_PhoneTypes_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_DocumentTypes_ResponsibleDocumentTypeId",
                        column: x => x.ResponsibleDocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentDisability",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    DisabilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentDisability", x => new { x.EnrollmentId, x.DisabilityId });
                    table.ForeignKey(
                        name: "FK_EnrollmentDisability_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentDisability_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDisability_DisabilityId",
                table: "EnrollmentDisability",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_AddressTypeId",
                table: "Enrollments",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_BirthStateId",
                table: "Enrollments",
                column: "BirthStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CivilStatusId",
                table: "Enrollments",
                column: "CivilStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CountryOfGraduationFromHighSchoolId",
                table: "Enrollments",
                column: "CountryOfGraduationFromHighSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CountryStateId",
                table: "Enrollments",
                column: "CountryStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GenderId",
                table: "Enrollments",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GuarantorDocumentTypeId",
                table: "Enrollments",
                column: "GuarantorDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_NationalityId",
                table: "Enrollments",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_OriginCountryId",
                table: "Enrollments",
                column: "OriginCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_PhoneTypeId",
                table: "Enrollments",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_RaceId",
                table: "Enrollments",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ResponsibleDocumentTypeId",
                table: "Enrollments",
                column: "ResponsibleDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SchoolId",
                table: "Enrollments",
                column: "SchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentDisability");

            migrationBuilder.DropTable(
                name: "Disabilities");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "AddressTypes");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "CivilStatus");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "PhoneTypes");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
