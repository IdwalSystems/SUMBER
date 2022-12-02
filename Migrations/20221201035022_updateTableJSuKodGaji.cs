using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class updateTableJSuKodGaji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Jenis",
                table: "JSuKodGaji",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jenis",
                table: "JSuKodGaji");
        }
    }
}
