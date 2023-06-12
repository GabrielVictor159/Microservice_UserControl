using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Infraestructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "onlineStatus",
                schema: "userControl",
                table: "Users",
                newName: "OnlineStatus");

            migrationBuilder.RenameColumn(
                name: "dateOfBirth",
                schema: "userControl",
                table: "Users",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OnlineStatus",
                schema: "userControl",
                table: "Users",
                newName: "onlineStatus");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                schema: "userControl",
                table: "Users",
                newName: "dateOfBirth");
        }
    }
}
