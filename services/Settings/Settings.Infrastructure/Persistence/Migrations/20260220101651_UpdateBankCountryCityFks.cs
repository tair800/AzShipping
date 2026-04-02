using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Settings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBankCountryCityFks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Banks");

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Banks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Banks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CityId",
                table: "Banks",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CountryId",
                table: "Banks",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Cities_CityId",
                table: "Banks",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Countries_CountryId",
                table: "Banks",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Cities_CityId",
                table: "Banks");

            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Countries_CountryId",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_CityId",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_CountryId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Banks");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Banks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Banks",
                type: "text",
                nullable: true);
        }
    }
}
