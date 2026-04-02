using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Accounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationActs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationActs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationInvoiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Payer = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    OrderNo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActNo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    ActDischargeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActSumWithoutVatAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    ActSumWithoutVatCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    ActSumWithVatAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    ActSumWithVatCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    InvoiceNo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    ActInvoiceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActInvoiceSumWithoutVatAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    ActInvoiceSumWithoutVatCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    ActInvoiceSumWithVatAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    ActInvoiceSumWithVatCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    BasicCurrencyWithoutVatAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    BasicCurrencyWithoutVatCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    BasicCurrencyWithVatAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    BasicCurrencyWithVatCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    BalancePaidAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    BalanceTotalAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    BalanceCurrency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationActs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationActs_OperationInvoices_OperationInvoiceId",
                        column: x => x.OperationInvoiceId,
                        principalTable: "OperationInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationActs_ActNo",
                table: "OperationActs",
                column: "ActNo");

            migrationBuilder.CreateIndex(
                name: "IX_OperationActs_OperationInvoiceId",
                table: "OperationActs",
                column: "OperationInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationActs");
        }
    }
}
