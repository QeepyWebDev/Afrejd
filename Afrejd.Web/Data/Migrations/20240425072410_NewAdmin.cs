using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afrejd.Web.Migrations
{
    /// <inheritdoc />
    public partial class NewAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId, ApplicationUserId) VALUES ('448713cb-0a8c-470a-b62d-0825e0debebb', '1', '448713cb-0a8c-470a-b62d-0825e0debebb')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("DELETE FROM AspNetUserRoles WHERE UserId = '448713cb-0a8c-470a-b62d-0825e0debebb'");
        }
    }
}
