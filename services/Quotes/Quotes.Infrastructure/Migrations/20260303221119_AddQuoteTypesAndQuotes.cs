using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteTypesAndQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuoteTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Direction = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Mode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    QuoteNumberPrefix = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CarrierApiPath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CarrierLabel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuoteNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    QuoteTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LogisticianId = table.Column<Guid>(type: "uuid", nullable: true),
                    LogisticianName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepartmentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ShipperId = table.Column<Guid>(type: "uuid", nullable: true),
                    ShipperName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    ConsigneeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConsigneeName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    MyCustomerTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    MyCustomerTypeName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    RateType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IncotermId = table.Column<Guid>(type: "uuid", nullable: true),
                    IncotermName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ExpirationDays = table.Column<int>(type: "integer", nullable: true),
                    MoveTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    MoveTypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CloseAutomaticallyDeclined = table.Column<bool>(type: "boolean", nullable: true),
                    CloseAutomaticallyDeclinedDays = table.Column<int>(type: "integer", nullable: true),
                    AutomaticallyCloseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PriceStandard = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    PriceWithVat = table.Column<decimal>(type: "numeric", nullable: true),
                    VatRate = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IncludePickup = table.Column<bool>(type: "boolean", nullable: false),
                    PickupCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    PickupCountryName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PickupStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    PickupStateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PickupCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    PickupCityName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PickupZipCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    GatewayTerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    GatewayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ViaPortTerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    ViaPortName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DestinationTerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    DestinationName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ViaPort2TerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    ViaPort2Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarrierName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IncludeDelivery = table.Column<bool>(type: "boolean", nullable: false),
                    DeliveryCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeliveryCountryName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DeliveryStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeliveryStateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DeliveryCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeliveryCityName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DeliveryZipCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    GrossWeightKg = table.Column<decimal>(type: "numeric", nullable: true),
                    VolumeCbm = table.Column<decimal>(type: "numeric", nullable: true),
                    ChargeableWeightKg = table.Column<decimal>(type: "numeric", nullable: true),
                    NumberOfPackages = table.Column<int>(type: "integer", nullable: true),
                    DangerousGoods = table.Column<bool>(type: "boolean", nullable: false),
                    DescriptionOfGoods = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ShipperRef2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ConsigneeRef2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: true),
                    AgentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NotesToBePrinted = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Notes = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_QuoteTypes_QuoteTypeId",
                        column: x => x.QuoteTypeId,
                        principalTable: "QuoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_QuoteTypeId",
                table: "Quotes",
                column: "QuoteTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "QuoteTypes");
        }
    }
}
