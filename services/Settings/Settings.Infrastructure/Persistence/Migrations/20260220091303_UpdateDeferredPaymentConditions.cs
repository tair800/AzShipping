using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Settings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeferredPaymentConditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrierDaysOfDelay",
                table: "DeferredPaymentConditions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CarrierIncluded",
                table: "DeferredPaymentConditions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ClientDaysOfDelay",
                table: "DeferredPaymentConditions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ClientIncluded",
                table: "DeferredPaymentConditions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarrierDaysOfDelay",
                table: "DeferredPaymentConditions");

            migrationBuilder.DropColumn(
                name: "CarrierIncluded",
                table: "DeferredPaymentConditions");

            migrationBuilder.DropColumn(
                name: "ClientDaysOfDelay",
                table: "DeferredPaymentConditions");

            migrationBuilder.DropColumn(
                name: "ClientIncluded",
                table: "DeferredPaymentConditions");
        }
    }
}
