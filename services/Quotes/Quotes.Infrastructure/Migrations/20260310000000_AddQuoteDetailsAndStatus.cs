using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Quotes.Infrastructure.Persistence;

#nullable disable

namespace Quotes.Infrastructure.Migrations;

[DbContext(typeof(QuotesDbContext))]
[Migration("20260310000000_AddQuoteDetailsAndStatus")]
public partial class AddQuoteDetailsAndStatus : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "PurchaseFreeDays",
            table: "Quotes",
            type: "integer",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "SaleFreeDays",
            table: "Quotes",
            type: "integer",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "QuoteStatus",
            table: "Quotes",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "VasId",
            table: "Quotes",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "HandlerId",
            table: "Quotes",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "HandlerName",
            table: "Quotes",
            type: "character varying(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "AccountManagerId",
            table: "Quotes",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "AccountManagerName",
            table: "Quotes",
            type: "character varying(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "OpenedById",
            table: "Quotes",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "OpenedByName",
            table: "Quotes",
            type: "character varying(200)",
            maxLength: 200,
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "PurchaseFreeDays", table: "Quotes");
        migrationBuilder.DropColumn(name: "SaleFreeDays", table: "Quotes");
        migrationBuilder.DropColumn(name: "QuoteStatus", table: "Quotes");
        migrationBuilder.DropColumn(name: "VasId", table: "Quotes");
        migrationBuilder.DropColumn(name: "HandlerId", table: "Quotes");
        migrationBuilder.DropColumn(name: "HandlerName", table: "Quotes");
        migrationBuilder.DropColumn(name: "AccountManagerId", table: "Quotes");
        migrationBuilder.DropColumn(name: "AccountManagerName", table: "Quotes");
        migrationBuilder.DropColumn(name: "OpenedById", table: "Quotes");
        migrationBuilder.DropColumn(name: "OpenedByName", table: "Quotes");
    }
}
