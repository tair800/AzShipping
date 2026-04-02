using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace General.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveApplyAutomaticallyFromVas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyAutomatically",
                table: "Vas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplyAutomatically",
                table: "Vas",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
