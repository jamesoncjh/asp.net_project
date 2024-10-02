﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Events.Data;

#nullable disable

namespace ThAmCo.Events.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    [Migration("20220610134531_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("thamco.events")
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ThAmCo.Events.Data.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers", "thamco.events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "bob@example.com",
                            FirstName = "Robert",
                            Surname = "Robertson"
                        },
                        new
                        {
                            Id = 2,
                            Email = "betty@example.com",
                            FirstName = "Betty",
                            Surname = "Thornton"
                        },
                        new
                        {
                            Id = 3,
                            Email = "jin@example.com",
                            FirstName = "Jin",
                            Surname = "Jellybeans"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nchar(3)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Events", "thamco.events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2016, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Duration = new TimeSpan(0, 6, 0, 0, 0),
                            Title = "Bob's Big 50",
                            TypeId = "PTY"
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2018, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Duration = new TimeSpan(0, 12, 0, 0, 0),
                            Title = "Best Wedding Yet",
                            TypeId = "WED"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<bool>("Attended")
                        .HasColumnType("bit");

                    b.HasKey("CustomerId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("Guests", "thamco.events");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            EventId = 1,
                            Attended = true
                        },
                        new
                        {
                            CustomerId = 2,
                            EventId = 1,
                            Attended = false
                        },
                        new
                        {
                            CustomerId = 1,
                            EventId = 2,
                            Attended = false
                        },
                        new
                        {
                            CustomerId = 3,
                            EventId = 2,
                            Attended = false
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staff", "thamco.events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "dickson@staff.example.com",
                            FirstName = "Dickson",
                            Surname = "Lee"
                        },
                        new
                        {
                            Id = 2,
                            Email = "anson@staff.example.com",
                            FirstName = "Anson",
                            Surname = "Ang"
                        },
                        new
                        {
                            Id = 3,
                            Email = "jamie@staff.example.com",
                            FirstName = "Jamie",
                            Surname = "Lim"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<bool>("Attended")
                        .HasColumnType("bit");

                    b.HasKey("StaffId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("Staffing", "thamco.events");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany("Bookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Data.Staff", "Staff")
                        .WithMany("StaffingList")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Customer", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Event", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staff", b =>
                {
                    b.Navigation("StaffingList");
                });
#pragma warning restore 612, 618
        }
    }
}
