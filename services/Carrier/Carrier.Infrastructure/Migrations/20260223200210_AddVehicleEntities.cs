using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EuroEmissionClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EuroEmissionClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: true),
                    EuroEmissionClassId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransportTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    TrailerNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BodyNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LicenceNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Drivers = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FuelCard = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransportInformation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TechPassportValidity = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LicenceValidityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OwnTransport = table.Column<bool>(type: "boolean", nullable: false),
                    VehicleFullWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    VehicleEmptyWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    Length = table.Column<decimal>(type: "numeric", nullable: true),
                    Width = table.Column<decimal>(type: "numeric", nullable: true),
                    Height = table.Column<decimal>(type: "numeric", nullable: true),
                    VehicleAxles = table.Column<int>(type: "integer", nullable: true),
                    MaxWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxEuroPallets = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EuroEmissionClasses");

            migrationBuilder.DropTable(
                name: "VehicleBrands");

            migrationBuilder.DropTable(
                name: "VehicleGroups");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
