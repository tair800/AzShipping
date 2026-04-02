using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clients.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameShipperConsigneeToShipperClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipperConsigneeNotRequired",
                table: "Clients",
                newName: "ShipperClientNotRequired");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipperClientNotRequired",
                table: "Clients",
                newName: "ShipperConsigneeNotRequired");
        }
    }
}
