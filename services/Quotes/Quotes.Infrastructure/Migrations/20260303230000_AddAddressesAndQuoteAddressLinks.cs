using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressesAndQuoteAddressLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AddressTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    AddressTypeName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Address1 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Address2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CountryName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    StateId = table.Column<Guid>(type: "uuid", nullable: true),
                    StateName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Fax = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Attn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    FullAddressDisplay = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "PickupAddressId",
                table: "Quotes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryAddressId",
                table: "Quotes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_PickupAddressId",
                table: "Quotes",
                column: "PickupAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_DeliveryAddressId",
                table: "Quotes",
                column: "DeliveryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Addresses_PickupAddressId",
                table: "Quotes",
                column: "PickupAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Addresses_DeliveryAddressId",
                table: "Quotes",
                column: "DeliveryAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Addresses_PickupAddressId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Addresses_DeliveryAddressId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_PickupAddressId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_DeliveryAddressId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "PickupAddressId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                table: "Quotes");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
