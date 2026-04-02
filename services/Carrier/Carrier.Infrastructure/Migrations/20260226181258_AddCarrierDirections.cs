using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarrierDirections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrierDirections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartureCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepartureGlobalZoneId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepartureCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArrivalCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArrivalGlobalZoneId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArrivalCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarrierLicences = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Comments = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierDirections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierDirections_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrierDirectionTransportTypes",
                columns: table => new
                {
                    CarrierDirectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransportTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierDirectionTransportTypes", x => new { x.CarrierDirectionId, x.TransportTypeId });
                    table.ForeignKey(
                        name: "FK_CarrierDirectionTransportTypes_CarrierDirections_CarrierDir~",
                        column: x => x.CarrierDirectionId,
                        principalTable: "CarrierDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrierDirections_CarrierId",
                table: "CarrierDirections",
                column: "CarrierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrierDirectionTransportTypes");

            migrationBuilder.DropTable(
                name: "CarrierDirections");
        }
    }
}
