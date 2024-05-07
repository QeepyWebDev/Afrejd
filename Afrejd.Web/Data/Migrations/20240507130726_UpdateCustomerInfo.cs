using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afrejd.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE CustomerInfo SET FirstName = 'TestName', OrganizationNumber = '837722-0733' WHERE Id = 20;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
