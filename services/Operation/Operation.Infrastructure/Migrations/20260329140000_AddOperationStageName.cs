using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Operation.Infrastructure.Persistence;

#nullable disable

namespace Operation.Infrastructure.Migrations
{
    [DbContext(typeof(OperationDbContext))]
    [Migration("20260329140000_AddOperationStageName")]
    /// <inheritdoc />
    public class AddOperationStageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OperationStageName",
                table: "Operations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationStageName",
                table: "Operations");
        }
    }
}
