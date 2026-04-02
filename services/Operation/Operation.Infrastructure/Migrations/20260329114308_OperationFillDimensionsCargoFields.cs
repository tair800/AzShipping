using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Operation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OperationFillDimensionsCargoFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CargoAdditionalInformation",
                table: "Operations",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoName",
                table: "Operations",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CargoTransportTypeId",
                table: "Operations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoTransportTypeName",
                table: "Operations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsignmentCurrencyCode",
                table: "Operations",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConsignmentCurrencyId",
                table: "Operations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConsignmentPrice",
                table: "Operations",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LoadingMethodId",
                table: "Operations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoadingMethodName",
                table: "Operations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PackageType",
                table: "OperationDimensions",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoAdditionalInformation",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CargoName",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CargoTransportTypeId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CargoTransportTypeName",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ConsignmentCurrencyCode",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ConsignmentCurrencyId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ConsignmentPrice",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "LoadingMethodId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "LoadingMethodName",
                table: "Operations");

            migrationBuilder.AlterColumn<string>(
                name: "PackageType",
                table: "OperationDimensions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
