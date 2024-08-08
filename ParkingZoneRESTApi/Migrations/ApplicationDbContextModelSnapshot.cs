﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingZoneWebApi.DataAccess;

#nullable disable

namespace ParkingZoneWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ParkingZoneWebApi.Models.ParkingSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("No")
                        .HasColumnType("int");

                    b.Property<int>("ParkingZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParkingZoneId");

                    b.ToTable("ParkingSlot");
                });

            modelBuilder.Entity("ParkingZoneWebApi.Models.ParkingZone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("ParkingZone");
                });

            modelBuilder.Entity("ParkingZoneWebApi.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("ParkingSlotId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Started")
                        .HasColumnType("datetime2");

                    b.Property<string>("VehicleNO")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("ParkingSlotId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("ParkingZoneWebApi.Models.ParkingSlot", b =>
                {
                    b.HasOne("ParkingZoneWebApi.Models.ParkingZone", "ParkingZone")
                        .WithMany("ParkingSlots")
                        .HasForeignKey("ParkingZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingZone");
                });

            modelBuilder.Entity("ParkingZoneWebApi.Models.Reservation", b =>
                {
                    b.HasOne("ParkingZoneWebApi.Models.ParkingSlot", "ParkingSlot")
                        .WithMany("Reservations")
                        .HasForeignKey("ParkingSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingSlot");
                });

            modelBuilder.Entity("ParkingZoneWebApi.Models.ParkingSlot", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("ParkingZoneWebApi.Models.ParkingZone", b =>
                {
                    b.Navigation("ParkingSlots");
                });
#pragma warning restore 612, 618
        }
    }
}