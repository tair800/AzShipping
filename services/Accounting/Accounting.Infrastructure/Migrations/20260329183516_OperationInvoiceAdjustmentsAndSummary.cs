using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OperationInvoiceAdjustmentsAndSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HeaderAdditions",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderAmountInExchange",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderDiscount",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderGeneralTotal",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderLineTotal",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderNetTotal",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderRounding",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderStoppage",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderTaxInclusiveTotal",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderTaxTotal",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeaderVatExemption",
                table: "OperationInvoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "OperationInvoiceDiscountLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    TypeCode = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Percent = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AllowanceChargeReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInvoiceDiscountLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInvoiceDiscountLines_OperationInvoices_OperationIn~",
                        column: x => x.OperationInvoiceId,
                        principalTable: "OperationInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationInvoiceNoteLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatorDisplayName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    NoteTypeCode = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    NoteText = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInvoiceNoteLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInvoiceNoteLines_OperationInvoices_OperationInvoic~",
                        column: x => x.OperationInvoiceId,
                        principalTable: "OperationInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationInvoicePaymentLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    AppcardName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    ConvertedAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    CurrencyCode = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    CurrencyRate = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: true),
                    PersonName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInvoicePaymentLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInvoicePaymentLines_OperationInvoices_OperationInv~",
                        column: x => x.OperationInvoiceId,
                        principalTable: "OperationInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationInvoiceTaxLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    TaxableAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxTypeCode = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    TaxPercent = table.Column<decimal>(type: "numeric(8,4)", precision: 8, scale: 4, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    FinalAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ExemptAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Rounding = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    AccountCode = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInvoiceTaxLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInvoiceTaxLines_OperationInvoices_OperationInvoice~",
                        column: x => x.OperationInvoiceId,
                        principalTable: "OperationInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationInvoiceDiscountLines_OperationInvoiceId",
                table: "OperationInvoiceDiscountLines",
                column: "OperationInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInvoiceNoteLines_OperationInvoiceId",
                table: "OperationInvoiceNoteLines",
                column: "OperationInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInvoicePaymentLines_OperationInvoiceId",
                table: "OperationInvoicePaymentLines",
                column: "OperationInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInvoiceTaxLines_OperationInvoiceId",
                table: "OperationInvoiceTaxLines",
                column: "OperationInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationInvoiceDiscountLines");

            migrationBuilder.DropTable(
                name: "OperationInvoiceNoteLines");

            migrationBuilder.DropTable(
                name: "OperationInvoicePaymentLines");

            migrationBuilder.DropTable(
                name: "OperationInvoiceTaxLines");

            migrationBuilder.DropColumn(
                name: "HeaderAdditions",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderAmountInExchange",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderDiscount",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderGeneralTotal",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderLineTotal",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderNetTotal",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderRounding",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderStoppage",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderTaxInclusiveTotal",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderTaxTotal",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeaderVatExemption",
                table: "OperationInvoices");
        }
    }
}
