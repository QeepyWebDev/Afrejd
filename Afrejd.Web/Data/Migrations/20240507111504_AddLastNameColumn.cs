using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afrejd.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddLastNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CustomerInfo",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CustomerInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CustomerInfo");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "CustomerInfo",
                newName: "Name");
        }
    }
}
