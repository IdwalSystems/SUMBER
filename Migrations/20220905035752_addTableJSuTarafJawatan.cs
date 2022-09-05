using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class addTableJSuTarafJawatan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "JSuTarafJawatan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kod = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Perihal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FlHapus = table.Column<int>(type: "int", nullable: false),
                    TarHapus = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuPekerjaMasukId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarMasuk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuPekerjaKemaskiniId = table.Column<int>(type: "int", nullable: true),
                    UserIdKemaskini = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarKemaskini = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JSuTarafJawatan", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JSuTarafJawatan");

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
    }
}
