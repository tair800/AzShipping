using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Infrastructure.Migrations
{
    public partial class AddExportSeaFclFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PortOfDeliveryName",
                table: "Quotes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity1",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity2",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity3",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity4",
                table: "Quotes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageType1",
                table: "Quotes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageType2",
                table: "Quotes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageType3",
                table: "Quotes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageType4",
                table: "Quotes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "PortOfDeliveryName", table: "Quotes");
            migrationBuilder.DropColumn(name: "Quantity1", table: "Quotes");
            migrationBuilder.DropColumn(name: "Quantity2", table: "Quotes");
            migrationBuilder.DropColumn(name: "Quantity3", table: "Quotes");
            migrationBuilder.DropColumn(name: "Quantity4", table: "Quotes");
            migrationBuilder.DropColumn(name: "PackageType1", table: "Quotes");
            migrationBuilder.DropColumn(name: "PackageType2", table: "Quotes");
            migrationBuilder.DropColumn(name: "PackageType3", table: "Quotes");
            migrationBuilder.DropColumn(name: "PackageType4", table: "Quotes");
        }
    }
}
