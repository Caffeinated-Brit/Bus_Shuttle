using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bus_Shuttle.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAuthorizedDriver",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDriver",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAuthorizedDriver",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDriver",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "User");
        }
    }
}
