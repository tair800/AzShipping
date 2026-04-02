using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Quotes.Infrastructure.Persistence;

#nullable disable

namespace Quotes.Infrastructure.Migrations;

[DbContext(typeof(QuotesDbContext))]
[Migration("20260312000000_AddQuoteEtdEta")]
public partial class AddQuoteEtdEta : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "Etd",
            table: "Quotes",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "Eta",
            table: "Quotes",
            type: "timestamp with time zone",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Etd", table: "Quotes");
        migrationBuilder.DropColumn(name: "Eta", table: "Quotes");
    }
}
