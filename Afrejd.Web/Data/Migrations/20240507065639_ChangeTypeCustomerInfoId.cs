using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afrejd.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeCustomerInfoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerInfoId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerInfoId",
                table: "Orders",
                column: "CustomerInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerInfo_CustomerInfoId",
                table: "Orders",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CustomerInfo_CustomerInfoId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerInfoId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerInfoId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
