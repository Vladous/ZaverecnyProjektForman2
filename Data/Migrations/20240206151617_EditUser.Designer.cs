﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZaverecnyProjektForman2.Data;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240206151617_EditUser")]
    partial class EditUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastChange")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreatedId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserLastChangedId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserLastChangedId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.InsuranceContracts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InsuranceId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsuredFrom")
                        .HasColumnType("datetime2");

                    b.Property<int>("InsuredId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsuredUntil")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastChange")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreatedId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserLastChangedId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceId");

                    b.HasIndex("InsuredId");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserLastChangedId");

                    b.ToTable("InsuranceContracts");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.InsuranceEvents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventsCount")
                        .HasColumnType("int");

                    b.Property<decimal?>("FulfillmentAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("FulfillmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InsuranceContractsId")
                        .HasColumnType("int");

                    b.Property<int?>("InsuranceContractsId1")
                        .HasColumnType("int");

                    b.Property<int?>("InsuranceId")
                        .HasColumnType("int");

                    b.Property<int?>("InsuredId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastChange")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserCreatedId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserLastChangedId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceContractsId");

                    b.HasIndex("InsuranceContractsId1");

                    b.HasIndex("InsuranceId");

                    b.HasIndex("InsuredId");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserLastChangedId");

                    b.ToTable("InsuranceEvents");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.Insured", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastChange")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreatedId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserLastChangedId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserLastChangedId");

                    b.ToTable("Insureds");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.Insurance", b =>
                {
                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserCreated")
                        .WithMany()
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserLastChanged")
                        .WithMany()
                        .HasForeignKey("UserLastChangedId");

                    b.Navigation("UserCreated");

                    b.Navigation("UserLastChanged");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.InsuranceContracts", b =>
                {
                    b.HasOne("ZaverecnyProjektForman2.Models.Insurance", "Insurance")
                        .WithMany("InsuranceContracts")
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZaverecnyProjektForman2.Models.Insured", "Insured")
                        .WithMany("InsuranceContracts")
                        .HasForeignKey("InsuredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserCreated")
                        .WithMany()
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserLastChanged")
                        .WithMany()
                        .HasForeignKey("UserLastChangedId");

                    b.Navigation("Insurance");

                    b.Navigation("Insured");

                    b.Navigation("UserCreated");

                    b.Navigation("UserLastChanged");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.InsuranceEvents", b =>
                {
                    b.HasOne("ZaverecnyProjektForman2.Models.InsuranceContracts", null)
                        .WithMany("InsuranceEvents")
                        .HasForeignKey("InsuranceContractsId");

                    b.HasOne("ZaverecnyProjektForman2.Models.InsuranceEvents", "InsuranceContracts")
                        .WithMany()
                        .HasForeignKey("InsuranceContractsId1");

                    b.HasOne("ZaverecnyProjektForman2.Models.Insurance", "Insurance")
                        .WithMany("InsuranceEvents")
                        .HasForeignKey("InsuranceId");

                    b.HasOne("ZaverecnyProjektForman2.Models.Insured", "Insured")
                        .WithMany("InsuranceEvents")
                        .HasForeignKey("InsuredId");

                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserCreated")
                        .WithMany()
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserLastChanged")
                        .WithMany()
                        .HasForeignKey("UserLastChangedId");

                    b.Navigation("Insurance");

                    b.Navigation("InsuranceContracts");

                    b.Navigation("Insured");

                    b.Navigation("UserCreated");

                    b.Navigation("UserLastChanged");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.Insured", b =>
                {
                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserCreated")
                        .WithMany()
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("ZaverecnyProjektForman2.Models.ApplicationUser", "UserLastChanged")
                        .WithMany()
                        .HasForeignKey("UserLastChangedId");

                    b.Navigation("UserCreated");

                    b.Navigation("UserLastChanged");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.Insurance", b =>
                {
                    b.Navigation("InsuranceContracts");

                    b.Navigation("InsuranceEvents");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.InsuranceContracts", b =>
                {
                    b.Navigation("InsuranceEvents");
                });

            modelBuilder.Entity("ZaverecnyProjektForman2.Models.Insured", b =>
                {
                    b.Navigation("InsuranceContracts");

                    b.Navigation("InsuranceEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
