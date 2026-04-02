using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Surname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    MiddleName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Passport = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DrivingLicenceNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BankAccount = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FuelCard = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    PassportFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DrivingLicenceFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DateOfEmployment = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverCarriers",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverCarriers", x => new { x.DriverId, x.CarrierId });
                    table.ForeignKey(
                        name: "FK_DriverCarriers_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverCarriers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverDrivingLicenceCategories",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    DrivingLicenceCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverDrivingLicenceCategories", x => new { x.DriverId, x.DrivingLicenceCategoryId });
                    table.ForeignKey(
                        name: "FK_DriverDrivingLicenceCategories_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverCarriers_CarrierId",
                table: "DriverCarriers",
                column: "CarrierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverCarriers");

            migrationBuilder.DropTable(
                name: "DriverDrivingLicenceCategories");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
