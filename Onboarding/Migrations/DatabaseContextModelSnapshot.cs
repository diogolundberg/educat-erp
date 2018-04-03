﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Onboarding.Models;
using System;

namespace Onboarding.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Onboarding.Models.AddressKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("AddressKinds");
                });

            modelBuilder.Entity("Onboarding.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("StateId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Onboarding.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Onboarding.Models.Disability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Disabilities");
                });

            modelBuilder.Entity("Onboarding.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<int>("DocumentTypeId");

                    b.Property<string>("ExternalId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Onboarding.Models.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DocumentType");
                });

            modelBuilder.Entity("Onboarding.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AcademicApproval");

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<DateTime>("Deadline");

                    b.Property<string>("ExternalId");

                    b.Property<DateTime?>("FinanceApproval");

                    b.Property<DateTime?>("ReviewedAt");

                    b.Property<DateTime?>("SentAt");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Onboarding.Models.FinanceData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<int?>("EnrollmentId");

                    b.Property<string>("ExternalId");

                    b.Property<int?>("PaymentMethodId");

                    b.Property<int?>("PlanId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId")
                        .IsUnique()
                        .HasFilter("[EnrollmentId] IS NOT NULL");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("PlanId");

                    b.ToTable("FinanceDatas");
                });

            modelBuilder.Entity("Onboarding.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("Onboarding.Models.Guarantor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CityId");

                    b.Property<string>("ComplementAddress");

                    b.Property<string>("Cpf");

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("ExternalId");

                    b.Property<int?>("FinanceDataId");

                    b.Property<string>("Landline");

                    b.Property<string>("Name");

                    b.Property<string>("Neighborhood");

                    b.Property<string>("PhoneNumber");

                    b.Property<int?>("StateId");

                    b.Property<string>("StreetAddress");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("FinanceDataId");

                    b.HasIndex("StateId");

                    b.ToTable("Guarantors");
                });

            modelBuilder.Entity("Onboarding.Models.GuarantorDocument", b =>
                {
                    b.Property<int>("GuarantorId");

                    b.Property<int>("DocumentId");

                    b.HasKey("GuarantorId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("GuarantorDocument");
                });

            modelBuilder.Entity("Onboarding.Models.HighSchoolKind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("HighSchoolKinds");
                });

            modelBuilder.Entity("Onboarding.Models.MaritalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("MaritalStatuses");
                });

            modelBuilder.Entity("Onboarding.Models.Nationality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("Onboarding.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("Onboarding.Models.Pendency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("EnrollmentId");

                    b.Property<string>("ExternalId");

                    b.Property<int>("SectionId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId");

                    b.HasIndex("SectionId");

                    b.ToTable("Pendencies");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pendency");
                });

            modelBuilder.Entity("Onboarding.Models.PersonalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressKindId");

                    b.Property<string>("AssumedName");

                    b.Property<int?>("BirthCityId");

                    b.Property<int?>("BirthCountryId");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<int?>("BirthStateId");

                    b.Property<string>("CPF");

                    b.Property<int?>("CityId");

                    b.Property<string>("ComplementAddress");

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<int?>("EnrollmentId");

                    b.Property<string>("ExternalId");

                    b.Property<int?>("GenderId");

                    b.Property<string>("Handicap");

                    b.Property<int?>("HighSchoolGraduationCountryId");

                    b.Property<string>("HighSchoolGraduationYear");

                    b.Property<int?>("HighSchoolKindId");

                    b.Property<string>("Landline");

                    b.Property<int?>("MaritalStatusId");

                    b.Property<string>("MothersName");

                    b.Property<int?>("NationalityId");

                    b.Property<string>("Neighborhood");

                    b.Property<string>("PhoneNumber");

                    b.Property<int?>("RaceId");

                    b.Property<string>("RealName");

                    b.Property<int?>("StateId");

                    b.Property<string>("StreetAddress");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("Zipcode");

                    b.HasKey("Id");

                    b.HasIndex("AddressKindId");

                    b.HasIndex("BirthCityId");

                    b.HasIndex("BirthCountryId");

                    b.HasIndex("BirthStateId");

                    b.HasIndex("CityId");

                    b.HasIndex("EnrollmentId")
                        .IsUnique()
                        .HasFilter("[EnrollmentId] IS NOT NULL");

                    b.HasIndex("GenderId");

                    b.HasIndex("HighSchoolGraduationCountryId");

                    b.HasIndex("HighSchoolKindId");

                    b.HasIndex("MaritalStatusId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("RaceId");

                    b.HasIndex("StateId");

                    b.ToTable("PersonalDatas");
                });

            modelBuilder.Entity("Onboarding.Models.PersonalDataDisability", b =>
                {
                    b.Property<int>("PersonalDataId");

                    b.Property<int>("DisabilityId");

                    b.HasKey("PersonalDataId", "DisabilityId");

                    b.HasIndex("DisabilityId");

                    b.ToTable("PersonalDataDisability");
                });

            modelBuilder.Entity("Onboarding.Models.PersonalDataDocument", b =>
                {
                    b.Property<int>("PersonalDataId");

                    b.Property<int>("DocumentId");

                    b.HasKey("PersonalDataId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("PersonalDataDocument");
                });

            modelBuilder.Entity("Onboarding.Models.PersonalDataSpecialNeed", b =>
                {
                    b.Property<int>("PersonalDataId");

                    b.Property<int>("SpecialNeedId");

                    b.HasKey("PersonalDataId", "SpecialNeedId");

                    b.HasIndex("SpecialNeedId");

                    b.ToTable("PersonalDataSpecialNeed");
                });

            modelBuilder.Entity("Onboarding.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<DateTime>("DueDate");

                    b.Property<string>("ExternalId");

                    b.Property<int>("Installments");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Onboarding.Models.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("Onboarding.Models.Representative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CityId");

                    b.Property<string>("ComplementAddress");

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("ExternalId");

                    b.Property<int?>("FinanceDataId");

                    b.Property<string>("Landline");

                    b.Property<string>("Name");

                    b.Property<string>("Neighborhood");

                    b.Property<string>("PhoneNumber");

                    b.Property<int?>("StateId");

                    b.Property<string>("StreetAddress");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("FinanceDataId")
                        .IsUnique()
                        .HasFilter("[FinanceDataId] IS NOT NULL");

                    b.HasIndex("StateId");

                    b.ToTable("Representatives");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Representative");
                });

            modelBuilder.Entity("Onboarding.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Onboarding.Models.SpecialNeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<int>("DisabilityId");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("DisabilityId");

                    b.ToTable("SpecialNeeds");
                });

            modelBuilder.Entity("Onboarding.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedAt");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Onboarding.Models.GuarantorDocumentType", b =>
                {
                    b.HasBaseType("Onboarding.Models.DocumentType");


                    b.ToTable("GuarantorDocumentType");

                    b.HasDiscriminator().HasValue("GuarantorDocumentType");
                });

            modelBuilder.Entity("Onboarding.Models.PersonalDocumentType", b =>
                {
                    b.HasBaseType("Onboarding.Models.DocumentType");


                    b.ToTable("PersonalDocumentType");

                    b.HasDiscriminator().HasValue("PersonalDocumentType");
                });

            modelBuilder.Entity("Onboarding.Models.ResponsibleDocumentType", b =>
                {
                    b.HasBaseType("Onboarding.Models.DocumentType");


                    b.ToTable("ResponsibleDocumentType");

                    b.HasDiscriminator().HasValue("ResponsibleDocumentType");
                });

            modelBuilder.Entity("Onboarding.Models.AcademicPendency", b =>
                {
                    b.HasBaseType("Onboarding.Models.Pendency");


                    b.ToTable("AcademicPendency");

                    b.HasDiscriminator().HasValue("AcademicPendency");
                });

            modelBuilder.Entity("Onboarding.Models.FinancePendency", b =>
                {
                    b.HasBaseType("Onboarding.Models.Pendency");


                    b.ToTable("FinancePendency");

                    b.HasDiscriminator().HasValue("FinancePendency");
                });

            modelBuilder.Entity("Onboarding.Models.RepresentativeCompany", b =>
                {
                    b.HasBaseType("Onboarding.Models.Representative");

                    b.Property<string>("Cnpj");

                    b.Property<string>("Contact");

                    b.ToTable("RepresentativeCompany");

                    b.HasDiscriminator().HasValue("RepresentativeCompany");
                });

            modelBuilder.Entity("Onboarding.Models.RepresentativePerson", b =>
                {
                    b.HasBaseType("Onboarding.Models.Representative");

                    b.Property<string>("Cpf");

                    b.Property<string>("Relationship");

                    b.ToTable("RepresentativePerson");

                    b.HasDiscriminator().HasValue("RepresentativePerson");
                });

            modelBuilder.Entity("Onboarding.Models.City", b =>
                {
                    b.HasOne("Onboarding.Models.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboarding.Models.Document", b =>
                {
                    b.HasOne("Onboarding.Models.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboarding.Models.FinanceData", b =>
                {
                    b.HasOne("Onboarding.Models.Enrollment", "Enrollment")
                        .WithOne("FinanceData")
                        .HasForeignKey("Onboarding.Models.FinanceData", "EnrollmentId");

                    b.HasOne("Onboarding.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");

                    b.HasOne("Onboarding.Models.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId");
                });

            modelBuilder.Entity("Onboarding.Models.Guarantor", b =>
                {
                    b.HasOne("Onboarding.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Onboarding.Models.FinanceData", "FinanceData")
                        .WithMany("Guarantors")
                        .HasForeignKey("FinanceDataId");

                    b.HasOne("Onboarding.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Onboarding.Models.GuarantorDocument", b =>
                {
                    b.HasOne("Onboarding.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboarding.Models.Guarantor", "Guarantor")
                        .WithMany("GuarantorDocuments")
                        .HasForeignKey("GuarantorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboarding.Models.Pendency", b =>
                {
                    b.HasOne("Onboarding.Models.Enrollment", "Enrollment")
                        .WithMany("Pendencies")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboarding.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboarding.Models.PersonalData", b =>
                {
                    b.HasOne("Onboarding.Models.AddressKind", "AddressKind")
                        .WithMany()
                        .HasForeignKey("AddressKindId");

                    b.HasOne("Onboarding.Models.City", "BirthCity")
                        .WithMany()
                        .HasForeignKey("BirthCityId");

                    b.HasOne("Onboarding.Models.Country", "BirthCountry")
                        .WithMany()
                        .HasForeignKey("BirthCountryId");

                    b.HasOne("Onboarding.Models.State", "BirthState")
                        .WithMany()
                        .HasForeignKey("BirthStateId");

                    b.HasOne("Onboarding.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Onboarding.Models.Enrollment", "Enrollment")
                        .WithOne("PersonalData")
                        .HasForeignKey("Onboarding.Models.PersonalData", "EnrollmentId");

                    b.HasOne("Onboarding.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("Onboarding.Models.Country", "HighSchoolGraduationCountry")
                        .WithMany()
                        .HasForeignKey("HighSchoolGraduationCountryId");

                    b.HasOne("Onboarding.Models.HighSchoolKind", "HighSchollKind")
                        .WithMany()
                        .HasForeignKey("HighSchoolKindId");

                    b.HasOne("Onboarding.Models.MaritalStatus", "MaritalStatus")
                        .WithMany()
                        .HasForeignKey("MaritalStatusId");

                    b.HasOne("Onboarding.Models.Nationality", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId");

                    b.HasOne("Onboarding.Models.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId");

                    b.HasOne("Onboarding.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Onboarding.Models.PersonalDataDisability", b =>
                {
                    b.HasOne("Onboarding.Models.Disability", "Disability")
                        .WithMany()
                        .HasForeignKey("DisabilityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboarding.Models.PersonalData", "PersonalData")
                        .WithMany("PersonalDataDisabilities")
                        .HasForeignKey("PersonalDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboarding.Models.PersonalDataDocument", b =>
                {
                    b.HasOne("Onboarding.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboarding.Models.PersonalData", "PersonalData")
                        .WithMany("PersonalDataDocuments")
                        .HasForeignKey("PersonalDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboarding.Models.PersonalDataSpecialNeed", b =>
                {
                    b.HasOne("Onboarding.Models.PersonalData", "PersonalData")
                        .WithMany("PersonalDataSpecialNeeds")
                        .HasForeignKey("PersonalDataId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboarding.Models.SpecialNeed", "SpecialNeed")
                        .WithMany()
                        .HasForeignKey("SpecialNeedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Onboarding.Models.Representative", b =>
                {
                    b.HasOne("Onboarding.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Onboarding.Models.FinanceData", "FinanceData")
                        .WithOne("Representative")
                        .HasForeignKey("Onboarding.Models.Representative", "FinanceDataId");

                    b.HasOne("Onboarding.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Onboarding.Models.SpecialNeed", b =>
                {
                    b.HasOne("Onboarding.Models.Disability", "Disability")
                        .WithMany("SpecialNeeds")
                        .HasForeignKey("DisabilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
