using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Infrastructure.Migrations
{
    public partial class AddTransitAirExpressFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RmbVwt",
                table: "Quotes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MyPortTerminalId",
                table: "Quotes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyPortName",
                table: "Quotes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MyPort2TerminalId",
                table: "Quotes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyPort2Name",
                table: "Quotes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "RmbVwt", table: "Quotes");
            migrationBuilder.DropColumn(name: "MyPortTerminalId", table: "Quotes");
            migrationBuilder.DropColumn(name: "MyPortName", table: "Quotes");
            migrationBuilder.DropColumn(name: "MyPort2TerminalId", table: "Quotes");
            migrationBuilder.DropColumn(name: "MyPort2Name", table: "Quotes");
        }
    }
}
