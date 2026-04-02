using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVatNoteForTransitRoadLtl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VatNote",
                table: "Quotes",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatNote",
                table: "Quotes");
        }
    }
}
