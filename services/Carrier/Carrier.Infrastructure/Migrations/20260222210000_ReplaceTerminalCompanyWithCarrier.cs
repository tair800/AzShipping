using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTerminalCompanyWithCarrier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "Terminals",
                type: "uuid",
                nullable: true);

            // Assign existing terminals to first carrier, or delete if none exist
            migrationBuilder.Sql(@"
                UPDATE ""Terminals"" t
                SET ""CarrierId"" = (SELECT ""Id"" FROM ""Carriers"" LIMIT 1)
                WHERE t.""CarrierId"" IS NULL AND EXISTS (SELECT 1 FROM ""Carriers"" LIMIT 1);
            ");
            migrationBuilder.Sql(@"DELETE FROM ""Terminals"" WHERE ""CarrierId"" IS NULL;");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarrierId",
                table: "Terminals",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Terminals");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Terminals",
                type: "uuid",
                nullable: true);
        }
    }
}
