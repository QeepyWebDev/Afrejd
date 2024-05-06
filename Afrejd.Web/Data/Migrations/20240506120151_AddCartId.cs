using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afrejd.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Carts");
        }
    }
}
