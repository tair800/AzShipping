using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Request.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExportAirRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportAirRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                    DispatchDateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DispatchDateTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UnloadingDateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UnloadingDateTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    QuotationSent = table.Column<bool>(type: "boolean", nullable: true),
                    StatusName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ExtremelyUrgent = table.Column<bool>(type: "boolean", nullable: false),
                    ToAnswerUntilDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PriceStandard = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    PriceWithVat = table.Column<decimal>(type: "numeric", nullable: true),
                    VatRate = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SourceOfRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceOfRequestName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    RequestPurposeId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequestPurposeName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    GatewayTerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    GatewayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ViaPortTerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    ViaPortName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DestinationTerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    DestinationName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ViaPort2TerminalId = table.Column<Guid>(type: "uuid", nullable: true),
                    ViaPort2Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AirlineId = table.Column<Guid>(type: "uuid", nullable: true),
                    AirlineName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    GrossWeightKg = table.Column<decimal>(type: "numeric", nullable: true),
                    VolumeCbm = table.Column<decimal>(type: "numeric", nullable: true),
                    ChargeableWeightKg = table.Column<decimal>(type: "numeric", nullable: true),
                    DangerousGoods = table.Column<bool>(type: "boolean", nullable: false),
                    DescriptionOfGoods = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Notes = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportAirRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExportAirRequestDimensions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExportAirRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Length = table.Column<decimal>(type: "numeric", nullable: false),
                    Width = table.Column<decimal>(type: "numeric", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    WeightKg = table.Column<decimal>(type: "numeric", nullable: true),
                    VolumeCbm = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportAirRequestDimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportAirRequestDimensions_ExportAirRequests_ExportAirReque~",
                        column: x => x.ExportAirRequestId,
                        principalTable: "ExportAirRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportAirRequestDimensions_ExportAirRequestId",
                table: "ExportAirRequestDimensions",
                column: "ExportAirRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportAirRequestDimensions");

            migrationBuilder.DropTable(
                name: "ExportAirRequests");
        }
    }
}
