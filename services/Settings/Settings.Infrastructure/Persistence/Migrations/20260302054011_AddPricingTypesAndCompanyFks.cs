using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Settings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingTypesAndCompanyFks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PricingTypeId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkerPostId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PricingTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CorrespondentCityId",
                table: "Companies",
                column: "CorrespondentCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CorrespondentCountryId",
                table: "Companies",
                column: "CorrespondentCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CorrespondentStateId",
                table: "Companies",
                column: "CorrespondentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PricingTypeId",
                table: "Companies",
                column: "PricingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_StateId",
                table: "Companies",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_WorkerPostId",
                table: "Companies",
                column: "WorkerPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Cities_CityId",
                table: "Companies",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Cities_CorrespondentCityId",
                table: "Companies",
                column: "CorrespondentCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Countries_CorrespondentCountryId",
                table: "Companies",
                column: "CorrespondentCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Countries_CountryId",
                table: "Companies",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_PricingTypes_PricingTypeId",
                table: "Companies",
                column: "PricingTypeId",
                principalTable: "PricingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_States_CorrespondentStateId",
                table: "Companies",
                column: "CorrespondentStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_States_StateId",
                table: "Companies",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_WorkerPosts_WorkerPostId",
                table: "Companies",
                column: "WorkerPostId",
                principalTable: "WorkerPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Cities_CityId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Cities_CorrespondentCityId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Countries_CorrespondentCountryId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Countries_CountryId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_PricingTypes_PricingTypeId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_States_CorrespondentStateId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_States_StateId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_WorkerPosts_WorkerPostId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "PricingTypes");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CityId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CorrespondentCityId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CorrespondentCountryId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CorrespondentStateId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CountryId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_PricingTypeId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_StateId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_WorkerPostId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PricingTypeId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WorkerPostId",
                table: "Companies");
        }
    }
}
