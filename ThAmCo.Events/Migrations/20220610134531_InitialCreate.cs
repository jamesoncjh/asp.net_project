﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thamco.events");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "thamco.events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "thamco.events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    TypeId = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                schema: "thamco.events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                schema: "thamco.events",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => new { x.CustomerId, x.EventId });
                    table.ForeignKey(
                        name: "FK_Guests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "thamco.events",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guests_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "thamco.events",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffing",
                schema: "thamco.events",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffing", x => new { x.StaffId, x.EventId });
                    table.ForeignKey(
                        name: "FK_Staffing_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "thamco.events",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staffing_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "thamco.events",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "Surname" },
                values: new object[,]
                {
                    { 1, "bob@example.com", "Robert", "Robertson" },
                    { 2, "betty@example.com", "Betty", "Thornton" },
                    { 3, "jin@example.com", "Jin", "Jellybeans" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Events",
                columns: new[] { "Id", "Date", "Duration", "Title", "TypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2016, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0), "Bob's Big 50", "PTY" },
                    { 2, new DateTime(2018, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), "Best Wedding Yet", "WED" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staff",
                columns: new[] { "Id", "Email", "FirstName", "Surname" },
                values: new object[,]
                {
                    { 1, "dickson@staff.example.com", "Dickson", "Lee" },
                    { 2, "anson@staff.example.com", "Anson", "Ang" },
                    { 3, "jamie@staff.example.com", "Jamie", "Lim" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Guests",
                columns: new[] { "CustomerId", "EventId", "Attended" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 1, 2, false },
                    { 2, 1, false },
                    { 3, 2, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_EventId",
                schema: "thamco.events",
                table: "Guests",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffing_EventId",
                schema: "thamco.events",
                table: "Staffing",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests",
                schema: "thamco.events");

            migrationBuilder.DropTable(
                name: "Staffing",
                schema: "thamco.events");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "thamco.events");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "thamco.events");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "thamco.events");
        }
    }
}
