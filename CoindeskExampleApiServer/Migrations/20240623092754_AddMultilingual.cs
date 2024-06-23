using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoindeskExampleApiServer.Migrations
{
    /// <inheritdoc />
    public partial class AddMultilingual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZWName",
                table: "Copilots",
                newName: "ZWName");

            migrationBuilder.AddColumn<string>(
                name: "JPName",
                table: "Copilots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "USName",
                table: "Copilots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JPName",
                table: "Copilots");

            migrationBuilder.DropColumn(
                name: "USName",
                table: "Copilots");

            migrationBuilder.RenameColumn(
                name: "ZWName",
                table: "Copilots",
                newName: "ZWName");
        }
    }
}
