using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace General.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyAndVasAmountApplyAuto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Vas",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ApplyAutomatically",
                table: "Vas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Vas",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    NumericCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vas_CurrencyId",
                table: "Vas",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vas_Currencies_CurrencyId",
                table: "Vas",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vas_Currencies_CurrencyId",
                table: "Vas");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Vas_CurrencyId",
                table: "Vas");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Vas");

            migrationBuilder.DropColumn(
                name: "ApplyAutomatically",
                table: "Vas");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Vas");
        }
    }
}
