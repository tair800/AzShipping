using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteStaffIdentityUserIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountManagerUserId",
                table: "Quotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HandlerUserId",
                table: "Quotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ManagerUserId",
                table: "Quotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OpenedByUserId",
                table: "Quotes",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountManagerUserId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "HandlerUserId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ManagerUserId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "OpenedByUserId",
                table: "Quotes");
        }
    }
}
