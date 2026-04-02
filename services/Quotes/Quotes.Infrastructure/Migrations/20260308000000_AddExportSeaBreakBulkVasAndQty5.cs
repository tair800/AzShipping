using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Infrastructure.Migrations
{
    public partial class AddExportSeaBreakBulkVasAndQty5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeVas",
                table: "Quotes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VasServiceName",
                table: "Quotes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExecutionPlace",
                table: "Quotes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VasQuantity",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VasUom",
                table: "Quotes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VasCurrencyCode",
                table: "Quotes",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VasTotal",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VasNotes",
                table: "Quotes",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity5",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageType5",
                table: "Quotes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IncludeVas", table: "Quotes");
            migrationBuilder.DropColumn(name: "VasServiceName", table: "Quotes");
            migrationBuilder.DropColumn(name: "ExecutionPlace", table: "Quotes");
            migrationBuilder.DropColumn(name: "VasQuantity", table: "Quotes");
            migrationBuilder.DropColumn(name: "VasUom", table: "Quotes");
            migrationBuilder.DropColumn(name: "VasCurrencyCode", table: "Quotes");
            migrationBuilder.DropColumn(name: "VasTotal", table: "Quotes");
            migrationBuilder.DropColumn(name: "VasNotes", table: "Quotes");
            migrationBuilder.DropColumn(name: "Quantity5", table: "Quotes");
            migrationBuilder.DropColumn(name: "PackageType5", table: "Quotes");
        }
    }
}
