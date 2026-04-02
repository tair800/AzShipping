using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace General.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OverWidth = table.Column<decimal>(type: "numeric", nullable: true),
                    OverHeight = table.Column<decimal>(type: "numeric", nullable: true),
                    OverWeight = table.Column<decimal>(type: "numeric", nullable: true),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    ExecutionPlace = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Uom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsAir = table.Column<bool>(type: "boolean", nullable: false),
                    IsSea = table.Column<bool>(type: "boolean", nullable: false),
                    IsRoad = table.Column<bool>(type: "boolean", nullable: false),
                    IsRail = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vas");
        }
    }
}
