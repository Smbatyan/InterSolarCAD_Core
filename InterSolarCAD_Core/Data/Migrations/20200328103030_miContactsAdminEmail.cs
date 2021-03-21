using Microsoft.EntityFrameworkCore.Migrations;

namespace InterSolarCAD_Core.Data.Migrations
{
    public partial class miContactsAdminEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "ContactUs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "ContactUs");
        }
    }
}
