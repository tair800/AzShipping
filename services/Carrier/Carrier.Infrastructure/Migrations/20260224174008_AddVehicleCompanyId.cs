using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleCompanyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Vehicles",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Vehicles");
        }
    }
}
