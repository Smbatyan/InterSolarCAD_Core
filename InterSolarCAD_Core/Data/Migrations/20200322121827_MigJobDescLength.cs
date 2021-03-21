using Microsoft.EntityFrameworkCore.Migrations;

namespace InterSolarCAD_Core.Data.Migrations
{
    public partial class MigJobDescLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Job",
                maxLength: 15000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1250)",
                oldMaxLength: 1250,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Job",
                type: "nvarchar(1250)",
                maxLength: 1250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15000,
                oldNullable: true);
        }
    }
}
