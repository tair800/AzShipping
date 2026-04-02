using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Operation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExportSeaFclPackageLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OceanBillOfLading",
                table: "Operations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortOfDeliveryName",
                table: "Operations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PortOfDeliveryTerminalId",
                table: "Operations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselId",
                table: "Operations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VesselName",
                table: "Operations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OperationPackageLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PackageType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationPackageLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationPackageLines_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationPackageLines_OperationId",
                table: "OperationPackageLines",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationPackageLines");

            migrationBuilder.DropColumn(
                name: "OceanBillOfLading",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PortOfDeliveryName",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PortOfDeliveryTerminalId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "VesselId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "VesselName",
                table: "Operations");
        }
    }
}
