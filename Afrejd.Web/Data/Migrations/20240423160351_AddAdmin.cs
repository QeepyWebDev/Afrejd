using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afrejd.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (1, 'Admin', 'ADMIN')");

            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('a41a1e9b-43eb-4e07-b5a7-22efe4941337', '1')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Name = 'Admin'");

            migrationBuilder.Sql("DELETE FROM AspNetUserRoles WHERE RoleId = '1'");
        }
    }
}
