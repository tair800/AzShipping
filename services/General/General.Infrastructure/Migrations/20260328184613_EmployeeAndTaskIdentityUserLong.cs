using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace General.Infrastructure.Migrations;

/// <inheritdoc />
public partial class EmployeeAndTaskIdentityUserLong : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // PostgreSQL cannot ALTER uuid → bigint; replace columns (clears old Guid values).
        migrationBuilder.Sql(@"UPDATE ""Tasks"" SET ""ResponsibleUserId"" = NULL;");
        migrationBuilder.Sql(@"ALTER TABLE ""Tasks"" DROP COLUMN ""ResponsibleUserId"";");
        migrationBuilder.Sql(@"ALTER TABLE ""Tasks"" ADD ""ResponsibleUserId"" bigint NULL;");

        migrationBuilder.DropIndex(name: "IX_Employees_UserId", table: "Employees");
        migrationBuilder.Sql(@"DELETE FROM ""Employees"";");
        migrationBuilder.Sql(@"ALTER TABLE ""Employees"" DROP COLUMN ""UserId"";");
        migrationBuilder.Sql(@"ALTER TABLE ""Employees"" ADD ""UserId"" bigint NOT NULL;");
        migrationBuilder.CreateIndex(
            name: "IX_Employees_UserId",
            table: "Employees",
            column: "UserId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(name: "IX_Employees_UserId", table: "Employees");
        migrationBuilder.Sql(@"ALTER TABLE ""Employees"" DROP COLUMN ""UserId"";");
        migrationBuilder.Sql(@"ALTER TABLE ""Employees"" ADD ""UserId"" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';");
        migrationBuilder.Sql(@"ALTER TABLE ""Employees"" ALTER COLUMN ""UserId"" DROP DEFAULT;");
        migrationBuilder.CreateIndex(
            name: "IX_Employees_UserId",
            table: "Employees",
            column: "UserId",
            unique: true);

        migrationBuilder.Sql(@"ALTER TABLE ""Tasks"" DROP COLUMN ""ResponsibleUserId"";");
        migrationBuilder.Sql(@"ALTER TABLE ""Tasks"" ADD ""ResponsibleUserId"" uuid NULL;");
    }
}
