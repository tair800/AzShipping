using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Quotes.Infrastructure.Persistence;

#nullable disable

namespace Quotes.Infrastructure.Migrations;

[DbContext(typeof(QuotesDbContext))]
[Migration("20260311000000_AddQuoteInsuranceAndGoodsFields")]
public partial class AddQuoteInsuranceAndGoodsFields : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IncludeInsurance",
            table: "Quotes",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<decimal>(
            name: "InsuranceValue",
            table: "Quotes",
            type: "numeric",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsStackable",
            table: "Quotes",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IncludeImportDutyCharges",
            table: "Quotes",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "TransitTime",
            table: "Quotes",
            type: "integer",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsFreighter",
            table: "Quotes",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "DepartureFrequency",
            table: "Quotes",
            type: "integer",
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "ValueOfGoods",
            table: "Quotes",
            type: "numeric",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "IncludeInsurance", table: "Quotes");
        migrationBuilder.DropColumn(name: "InsuranceValue", table: "Quotes");
        migrationBuilder.DropColumn(name: "IsStackable", table: "Quotes");
        migrationBuilder.DropColumn(name: "IncludeImportDutyCharges", table: "Quotes");
        migrationBuilder.DropColumn(name: "TransitTime", table: "Quotes");
        migrationBuilder.DropColumn(name: "IsFreighter", table: "Quotes");
        migrationBuilder.DropColumn(name: "DepartureFrequency", table: "Quotes");
        migrationBuilder.DropColumn(name: "ValueOfGoods", table: "Quotes");
    }
}
