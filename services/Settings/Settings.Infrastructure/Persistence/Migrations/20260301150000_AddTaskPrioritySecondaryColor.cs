using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Settings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskPrioritySecondaryColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondaryColor",
                table: "TaskPriorities",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "#ffffff");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "TaskPriorities",
                newName: "PrimaryColor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimaryColor",
                table: "TaskPriorities",
                newName: "Color");

            migrationBuilder.DropColumn(
                name: "SecondaryColor",
                table: "TaskPriorities");
        }
    }
}
