using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationInvoicesAndPaymentLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OperationInvoiceId",
                table: "Payments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OperationInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PayerName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CurrencyCode = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Notes = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false),
                    SubtotalExclVat = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    VatTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalInclVat = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentsBalanceCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    PaymentsBalanceAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationInvoiceLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    VatPercent = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    LineNet = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LineVat = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LineGross = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInvoiceLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInvoiceLines_OperationInvoices_OperationInvoiceId",
                        column: x => x.OperationInvoiceId,
                        principalTable: "OperationInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OperationInvoiceId",
                table: "Payments",
                column: "OperationInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInvoiceLines_OperationInvoiceId",
                table: "OperationInvoiceLines",
                column: "OperationInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_OperationInvoices_OperationInvoiceId",
                table: "Payments",
                column: "OperationInvoiceId",
                principalTable: "OperationInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_OperationInvoices_OperationInvoiceId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "OperationInvoiceLines");

            migrationBuilder.DropTable(
                name: "OperationInvoices");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OperationInvoiceId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OperationInvoiceId",
                table: "Payments");
        }
    }
}
