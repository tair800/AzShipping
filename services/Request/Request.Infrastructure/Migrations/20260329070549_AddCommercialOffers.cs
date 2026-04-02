using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Request.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommercialOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommercialOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProvideClientAccess = table.Column<bool>(type: "boolean", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    TemplateName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    BasedOnCalculation = table.Column<bool>(type: "boolean", nullable: false),
                    DocumentName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DocumentSourceType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AttachedFileReference = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Comments = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommercialOffers_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommercialOfferSelectedProposals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommercialOfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceProposalId = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialOfferSelectedProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommercialOfferSelectedProposals_CommercialOffers_Commercia~",
                        column: x => x.CommercialOfferId,
                        principalTable: "CommercialOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommercialOfferSelectedProposals_PriceProposals_PricePropos~",
                        column: x => x.PriceProposalId,
                        principalTable: "PriceProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommercialOffers_RequestId",
                table: "CommercialOffers",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CommercialOfferSelectedProposals_CommercialOfferId_PricePro~",
                table: "CommercialOfferSelectedProposals",
                columns: new[] { "CommercialOfferId", "PriceProposalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommercialOfferSelectedProposals_PriceProposalId",
                table: "CommercialOfferSelectedProposals",
                column: "PriceProposalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommercialOfferSelectedProposals");

            migrationBuilder.DropTable(
                name: "CommercialOffers");
        }
    }
}
