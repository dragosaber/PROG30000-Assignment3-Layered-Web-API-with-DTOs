using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventRegistrationAPI_Joseph_Thomas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Capacity", "EventDate", "Location", "Title" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2025, 9, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Main Hall", "Orientation Mixer" },
                    { 2, 100, new DateTime(2027, 3, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Gym", "Career Fair" },
                    { 3, 2, new DateTime(2027, 2, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), "Lab 204", "Git Workshop" },
                    { 4, 50, new DateTime(2027, 4, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "Innovation Hub", "Hackathon" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "RegistrationId", "Email", "EventId", "RegisteredOn", "StudentName" },
                values: new object[,]
                {
                    { 1, "jojo@gmail.com", 3, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joseph Thomas" },
                    { 2, "1@gmail.com", 3, new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Benjamin Franklin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
