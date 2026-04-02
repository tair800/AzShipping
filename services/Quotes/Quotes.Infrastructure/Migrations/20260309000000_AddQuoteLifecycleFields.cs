using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Quotes.Infrastructure.Persistence;

#nullable disable

namespace Quotes.Infrastructure.Migrations;

[DbContext(typeof(QuotesDbContext))]
[Migration("20260309000000_AddQuoteLifecycleFields")]
public partial class AddQuoteLifecycleFields : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsCancelled",
            table: "Quotes",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "CancelledAt",
            table: "Quotes",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "SentToCustomerAt",
            table: "Quotes",
            type: "timestamp with time zone",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "IsCancelled", table: "Quotes");
        migrationBuilder.DropColumn(name: "CancelledAt", table: "Quotes");
        migrationBuilder.DropColumn(name: "SentToCustomerAt", table: "Quotes");
    }
}

