using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrier.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarrierEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LocalName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    ClientAdsCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Okpo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Bin = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ogrn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Tin = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Rrc = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    VatNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CarrierTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransportTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LegalCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegalStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegalCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegalZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LegalPhones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LegalFax = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LegalEmails = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PostalCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostalStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostalCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostalZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PostalPhones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PostalFax = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PostalEmails = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreditLimit = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentDelay = table.Column<int>(type: "integer", nullable: true),
                    DeferredPaymentConditionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsDeactive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrierBankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    AccountNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BankId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransitAccount = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CorrespondentBank = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CorrespondentAccount = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierBankAccounts_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrierContactPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnglishName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Emails = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Phones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Fax = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierContactPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierContactPersons_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrierManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarrierId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierManagers_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBankAccounts_CarrierId",
                table: "CarrierBankAccounts",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierContactPersons_CarrierId",
                table: "CarrierContactPersons",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierManagers_CarrierId",
                table: "CarrierManagers",
                column: "CarrierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrierBankAccounts");

            migrationBuilder.DropTable(
                name: "CarrierContactPersons");

            migrationBuilder.DropTable(
                name: "CarrierManagers");

            migrationBuilder.DropTable(
                name: "Carriers");
        }
    }
}
