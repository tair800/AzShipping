using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Request.Infrastructure.Migrations
{
    public partial class AddSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HasSea = table.Column<bool>(type: "boolean", nullable: false),
                    HasAir = table.Column<bool>(type: "boolean", nullable: false),
                    HasRail = table.Column<bool>(type: "boolean", nullable: false),
                    HasRoad = table.Column<bool>(type: "boolean", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SubType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarrierName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SaleStatusId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CargoName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CargoVolume = table.Column<decimal>(type: "numeric", nullable: true),
                    CargoWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    CargoSize = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LoadingPlace = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    UnloadingPlace = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    DealValue = table.Column<decimal>(type: "numeric", nullable: true),
                    DealValueCurrency = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ManagerSellerName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    PriceProposal = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SaleListStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_SaleStatuses_SaleStatusId",
                        column: x => x.SaleStatusId,
                        principalTable: "SaleStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(name: "IX_Sales_SaleStatusId", table: "Sales", column: "SaleStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Sales");
        }
    }
}
