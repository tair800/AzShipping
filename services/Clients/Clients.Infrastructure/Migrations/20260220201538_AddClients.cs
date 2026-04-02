using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clients.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsCustomer = table.Column<bool>(type: "boolean", nullable: false),
                    ShipperConsigneeNotRequired = table.Column<bool>(type: "boolean", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NameAbbreviated = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: true),
                    SalesmanId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientSourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientStatusId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActivityAreaId = table.Column<Guid>(type: "uuid", nullable: true),
                    VatNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Inn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Okpo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Kpp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Bin = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ClientAisCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LegalCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegalStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegalCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    LegalZipCode = table.Column<string>(type: "text", nullable: true),
                    LegalStreet = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LegalEmail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LegalFax = table.Column<string>(type: "text", nullable: true),
                    PostalCountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostalStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostalCityId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostalZipCode = table.Column<string>(type: "text", nullable: true),
                    PostalStreet = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PostalEmail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PostalPhone = table.Column<string>(type: "text", nullable: true),
                    PostalMobile = table.Column<string>(type: "text", nullable: true),
                    PostalFax = table.Column<string>(type: "text", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "numeric", nullable: true),
                    DeferredPaymentConditionId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymentDelay = table.Column<int>(type: "integer", nullable: true),
                    EmailToSendDocuments = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsDeactive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientBankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    BankId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: true),
                    AccountNumberIban = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransitAmount = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CorrespondentBankId = table.Column<Guid>(type: "uuid", nullable: true),
                    CorrespondentAccount = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientBankAccounts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientContactPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnglishName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContactPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientContactPersons_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientBankAccounts_ClientId",
                table: "ClientBankAccounts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContactPersons_ClientId",
                table: "ClientContactPersons",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientBankAccounts");

            migrationBuilder.DropTable(
                name: "ClientContactPersons");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
