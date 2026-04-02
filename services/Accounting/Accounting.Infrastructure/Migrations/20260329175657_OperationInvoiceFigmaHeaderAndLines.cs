using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OperationInvoiceFigmaHeaderAndLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BreakingRule",
                table: "OperationInvoices",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNumber",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractorName",
                table: "OperationInvoices",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpenseCenterCode",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadCode",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceAccountCode",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceAddress",
                table: "OperationInvoices",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNote",
                table: "OperationInvoices",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTypeCode",
                table: "OperationInvoices",
                type: "character varying(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "IssueTime",
                table: "OperationInvoices",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "OperationInvoices",
                type: "character varying(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostponedDays",
                table: "OperationInvoices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PricingTypeCode",
                table: "OperationInvoices",
                type: "character varying(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicReference",
                table: "OperationInvoices",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialCode",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemplateCode",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCode",
                table: "OperationInvoices",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockCode",
                table: "OperationInvoiceLines",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxExemptionAmount",
                table: "OperationInvoiceLines",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "OperationInvoiceLines",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakingRule",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "ContractorName",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "ExpenseCenterCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "HeadCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceAccountCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceAddress",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceNote",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceTypeCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "IssueTime",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "PostponedDays",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "PricingTypeCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "PublicReference",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "SpecialCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "TemplateCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "WarehouseCode",
                table: "OperationInvoices");

            migrationBuilder.DropColumn(
                name: "StockCode",
                table: "OperationInvoiceLines");

            migrationBuilder.DropColumn(
                name: "TaxExemptionAmount",
                table: "OperationInvoiceLines");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "OperationInvoiceLines");
        }
    }
}
