using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSettingsBackedInvoiceLookupRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // PricingType=8, Warehouse=9, Head=10, Department=11, Language=12, Template=13 — sourced from Settings.UI merge.
            migrationBuilder.Sql(
                """
                DELETE FROM "InvoiceLookupOptions" WHERE "Category" IN (8, 9, 10, 11, 12, 13);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
