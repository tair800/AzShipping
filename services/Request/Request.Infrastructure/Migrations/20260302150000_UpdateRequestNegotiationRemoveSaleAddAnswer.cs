using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Request.Infrastructure.Migrations
{
    public partial class UpdateRequestNegotiationRemoveSaleAddAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestNegotiations_Sales_SaleId",
                table: "RequestNegotiations");

            migrationBuilder.DropIndex(
                name: "IX_RequestNegotiations_SaleId",
                table: "RequestNegotiations");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "RequestNegotiations");

            migrationBuilder.RenameColumn(
                name: "QuestionsAndAnswers",
                table: "RequestNegotiations",
                newName: "Question");

            migrationBuilder.Sql("UPDATE \"RequestNegotiations\" SET \"ClientId\" = '00000000-0000-0000-0000-000000000000' WHERE \"ClientId\" IS NULL;");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "RequestNegotiations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "RequestNegotiations",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "RequestNegotiations",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "RequestNegotiations");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "RequestNegotiations",
                newName: "QuestionsAndAnswers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "RequestNegotiations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionsAndAnswers",
                table: "RequestNegotiations",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleId",
                table: "RequestNegotiations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RequestNegotiations_SaleId",
                table: "RequestNegotiations",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestNegotiations_Sales_SaleId",
                table: "RequestNegotiations",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
