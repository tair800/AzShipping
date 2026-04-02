using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Settings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBankCodeSwiftToCompanyBankAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "CompanyBankAccounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Swift",
                table: "CompanyBankAccounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "CompanyBankAccounts");

            migrationBuilder.DropColumn(
                name: "Swift",
                table: "CompanyBankAccounts");
        }
    }
}
