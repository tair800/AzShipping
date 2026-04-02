using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Settings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExtendNumerationRuleDimensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentTypeCode",
                table: "Numerations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElementCode",
                table: "Numerations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Numerations_NumerationForCode_CompanyId_DepartmentId_Client~",
                table: "Numerations",
                columns: new[] { "NumerationForCode", "CompanyId", "DepartmentId", "ClientId", "EmployeeId", "ElementCode", "DocumentTypeCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Numerations_NumerationForCode_CompanyId_DepartmentId_Client~",
                table: "Numerations");

            migrationBuilder.DropColumn(
                name: "DocumentTypeCode",
                table: "Numerations");

            migrationBuilder.DropColumn(
                name: "ElementCode",
                table: "Numerations");
        }
    }
}
