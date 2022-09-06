using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class changeTableNameToJSuGredGaji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JSuGredGajis",
                table: "JSuGredGajis");

            migrationBuilder.RenameTable(
                name: "JSuGredGajis",
                newName: "JSuGredGaji");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JSuGredGaji",
                table: "JSuGredGaji",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JSuGredGaji",
                table: "JSuGredGaji");

            migrationBuilder.RenameTable(
                name: "JSuGredGaji",
                newName: "JSuGredGajis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JSuGredGajis",
                table: "JSuGredGajis",
                column: "Id");
        }
    }
}
