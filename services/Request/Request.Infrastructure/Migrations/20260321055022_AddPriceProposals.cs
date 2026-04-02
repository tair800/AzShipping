using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Request.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceProposals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceProposals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TemplateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarrierName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    TypeOfService = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    ClientPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    ClientPriceWithVat = table.Column<decimal>(type: "numeric", nullable: true),
                    ClientVatRateId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientVatRateCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ClientCurrencyId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientCurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    SeparateLineInInvoice = table.Column<bool>(type: "boolean", nullable: false),
                    CarrierRate = table.Column<decimal>(type: "numeric", nullable: true),
                    CarrierRateWithVat = table.Column<decimal>(type: "numeric", nullable: true),
                    CarrierVatRateId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarrierVatRateCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CarrierCurrencyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CarrierCurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Expense = table.Column<decimal>(type: "numeric", nullable: true),
                    Profit = table.Column<decimal>(type: "numeric", nullable: true),
                    Route = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Comments = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceProposals_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceProposalCargos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceProposalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    PackageType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IncludeInsurance = table.Column<bool>(type: "boolean", nullable: false),
                    DescriptionOfGoods = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceProposalCargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceProposalCargos_PriceProposals_PriceProposalId",
                        column: x => x.PriceProposalId,
                        principalTable: "PriceProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceProposalCargos_PriceProposalId",
                table: "PriceProposalCargos",
                column: "PriceProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceProposals_RequestId",
                table: "PriceProposals",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceProposalCargos");

            migrationBuilder.DropTable(
                name: "PriceProposals");
        }
    }
}
