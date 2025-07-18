﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250616085650_BloodSupportDb")]
    partial class BloodSupportDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Blood", b =>
                {
                    b.Property<Guid>("BloodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BloodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CollectedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ComponentType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<double?>("VolumeInML")
                        .HasColumnType("float");

                    b.HasKey("BloodId");

                    b.ToTable("Blood");
                });

            modelBuilder.Entity("DAL.Models.BloodRegistration", b =>
                {
                    b.Property<Guid>("BloodRegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DonationEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BloodRegistrationId");

                    b.HasIndex("DonationEventId");

                    b.HasIndex("UserId");

                    b.ToTable("BloodRegistrations");
                });

            modelBuilder.Entity("DAL.Models.BloodRequest", b =>
                {
                    b.Property<Guid>("BloodRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BloodGroup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ComponentType")
                        .HasColumnType("int");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RequestedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RequestedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("VolumeInML")
                        .HasColumnType("float");

                    b.HasKey("BloodRequestId");

                    b.ToTable("BloodRequests");
                });

            modelBuilder.Entity("DAL.Models.DonationEvent", b =>
                {
                    b.Property<Guid>("DonationEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DonationEventId");

                    b.ToTable("DonationEvent");
                });

            modelBuilder.Entity("DAL.Models.DonationHistory", b =>
                {
                    b.Property<Guid>("DonationHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BloodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DonationHistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("DonationHistorys");
                });

            modelBuilder.Entity("DAL.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshTokenKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.UserMedical", b =>
                {
                    b.Property<Guid>("UserMedicalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BloodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentHealthStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DonationCount")
                        .HasColumnType("int");

                    b.Property<bool>("HasTransmissibleDisease")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTakingMedication")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastDonationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserMedicalId");

                    b.HasIndex("BloodId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("UserMedicals");
                });

            modelBuilder.Entity("DAL.Models.BloodRegistration", b =>
                {
                    b.HasOne("DAL.Models.DonationEvent", "DonationEvent")
                        .WithMany("Registrations")
                        .HasForeignKey("DonationEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("BloodRegistrations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonationEvent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.DonationHistory", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("DonationHistorys")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.RefreshToken", b =>
                {
                    b.HasOne("DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.UserMedical", b =>
                {
                    b.HasOne("DAL.Models.Blood", "Blood")
                        .WithOne("UserMedicals")
                        .HasForeignKey("DAL.Models.UserMedical", "BloodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("UserMedicals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blood");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Blood", b =>
                {
                    b.Navigation("UserMedicals")
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.DonationEvent", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("BloodRegistrations");

                    b.Navigation("DonationHistorys");

                    b.Navigation("UserMedicals");
                });
#pragma warning restore 612, 618
        }
    }
}
