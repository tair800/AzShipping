using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Settings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EmailAccountSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailAccountSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountEmail = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    UseSeparateAuthLogin = table.Column<bool>(type: "boolean", nullable: false),
                    SmtpAuthUsername = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                    ProtectedPassword = table.Column<byte[]>(type: "bytea", nullable: true),
                    WithoutPassword = table.Column<bool>(type: "boolean", nullable: false),
                    ConnectionMode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SmtpHost = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SmtpPort = table.Column<int>(type: "integer", nullable: false),
                    SmtpSecurity = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsSystemEmail = table.Column<bool>(type: "boolean", nullable: false),
                    IdentityUserId = table.Column<long>(type: "bigint", nullable: true),
                    LinkedUserDisplayName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAccountSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailAccountSettings_AccountEmail",
                table: "EmailAccountSettings",
                column: "AccountEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAccountSettings");
        }
    }
}
