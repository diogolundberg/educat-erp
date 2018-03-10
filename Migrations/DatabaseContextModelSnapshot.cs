﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
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

            modelBuilder.Entity("Onboarding.Models.AddressType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("AddressTypes");
                });

            modelBuilder.Entity("Onboarding.Models.CivilStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("CivilStatus");
                });

            modelBuilder.Entity("Onboarding.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Onboarding.Models.Disability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("Disabilities");
                });

            modelBuilder.Entity("Onboarding.Models.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("State");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Onboarding.Models.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Address");

                    b.Property<Guid?>("AddressTypeId");

                    b.Property<DateTime>("Birthday");

                    b.Property<Guid?>("BornStateId");

                    b.Property<string>("Cep");

                    b.Property<string>("City");

                    b.Property<Guid?>("CivilStatusId");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<Guid?>("CountryOfGraduationFromHighSchoolId");

                    b.Property<Guid?>("CountryStateId");

                    b.Property<int>("Cpf");

                    b.Property<string>("Email");

                    b.Property<Guid?>("EmailId");

                    b.Property<int>("ExternalId");

                    b.Property<Guid?>("GenderId");

                    b.Property<bool?>("HaveHandcaps");

                    b.Property<string>("MotherMom");

                    b.Property<string>("Name");

                    b.Property<Guid?>("NationalityId");

                    b.Property<string>("Neighborhood");

                    b.Property<int?>("Number");

                    b.Property<Guid?>("OriginCountryId");

                    b.Property<string>("PhoneNumber");

                    b.Property<Guid?>("PhoneTypeId");

                    b.Property<Guid?>("RaceId");

                    b.Property<Guid?>("SchoolId");

                    b.Property<DateTimeOffset?>("SendBy");

                    b.Property<string>("SocialName");

                    b.Property<string>("State");

                    b.Property<int?>("YearofHighSchoolGraduation");

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.HasIndex("BornStateId");

                    b.HasIndex("CivilStatusId");

                    b.HasIndex("CountryOfGraduationFromHighSchoolId");

                    b.HasIndex("CountryStateId");

                    b.HasIndex("EmailId");

                    b.HasIndex("GenderId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("OriginCountryId");

                    b.HasIndex("PhoneTypeId");

                    b.HasIndex("RaceId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("Onboarding.Models.EnrollmentDisability", b =>
                {
                    b.Property<Guid>("EnrollmentId");

                    b.Property<Guid>("DisabilityId");

                    b.HasKey("EnrollmentId", "DisabilityId");

                    b.HasIndex("DisabilityId");

                    b.ToTable("EnrollmentDisability");
                });

            modelBuilder.Entity("Onboarding.Models.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("Onboarding.Models.Nationality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("Onboarding.Models.PhoneType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("PhoneTypes");
                });

            modelBuilder.Entity("Onboarding.Models.Race", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("Onboarding.Models.School", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Onboarding.Models.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CommitedBy");

                    b.Property<DateTime>("CommittedAt");

                    b.Property<int>("ExternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Onboarding.Models.Enrollment", b =>
                {
                    b.HasOne("Onboarding.Models.AddressType", "AddressType")
                        .WithMany("Enrollments")
                        .HasForeignKey("AddressTypeId");

                    b.HasOne("Onboarding.Models.State", "BornState")
                        .WithMany("BornStateEnrollments")
                        .HasForeignKey("BornStateId");

                    b.HasOne("Onboarding.Models.CivilStatus", "CivilStatus")
                        .WithMany("Enrollments")
                        .HasForeignKey("CivilStatusId");

                    b.HasOne("Onboarding.Models.Country", "CountryOfGraduationFromHighSchool")
                        .WithMany("CountryOfGraduationFromHighSchoolEnrollments")
                        .HasForeignKey("CountryOfGraduationFromHighSchoolId");

                    b.HasOne("Onboarding.Models.State", "CountryState")
                        .WithMany("CountryStateEnrollments")
                        .HasForeignKey("CountryStateId");

                    b.HasOne("Onboarding.Models.Email")
                        .WithMany("Enrollments")
                        .HasForeignKey("EmailId");

                    b.HasOne("Onboarding.Models.Gender", "Gender")
                        .WithMany("Enrollments")
                        .HasForeignKey("GenderId");

                    b.HasOne("Onboarding.Models.Nationality", "Nationality")
                        .WithMany("Enrollments")
                        .HasForeignKey("NationalityId");

                    b.HasOne("Onboarding.Models.Country", "OriginCountry")
                        .WithMany("OriginCountryEnrollments")
                        .HasForeignKey("OriginCountryId");

                    b.HasOne("Onboarding.Models.PhoneType", "PhoneType")
                        .WithMany("Enrollments")
                        .HasForeignKey("PhoneTypeId");

                    b.HasOne("Onboarding.Models.Race", "Race")
                        .WithMany("Enrollments")
                        .HasForeignKey("RaceId");

                    b.HasOne("Onboarding.Models.School", "School")
                        .WithMany("Enrollments")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("Onboarding.Models.EnrollmentDisability", b =>
                {
                    b.HasOne("Onboarding.Models.Disability", "Disability")
                        .WithMany("EnrollmentDisabilities")
                        .HasForeignKey("DisabilityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Onboarding.Models.Enrollment", "Enrollment")
                        .WithMany("EnrollmentDisabilities")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
