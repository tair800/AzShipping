using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCarrierTerminalCompanyRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terminals_Carriers_CarrierId",
                table: "Terminals");

            migrationBuilder.DropIndex(
                name: "IX_Terminals_CarrierId",
                table: "Terminals");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "Terminals");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Carriers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "Terminals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Carriers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terminals_CarrierId",
                table: "Terminals",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminals_Carriers_CarrierId",
                table: "Terminals",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
