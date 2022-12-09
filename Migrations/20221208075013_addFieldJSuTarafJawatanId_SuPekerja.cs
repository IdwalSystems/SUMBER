using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class addFieldJSuTarafJawatanId_SuPekerja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JSuTarafJawatanId",
                table: "SuPekerja",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JSuTarafJawatanSuPekerja",
                columns: table => new
                {
                    JSuTarafJawatanID = table.Column<int>(type: "int", nullable: false),
                    SuPekerjaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JSuTarafJawatanSuPekerja", x => new { x.JSuTarafJawatanID, x.SuPekerjaId });
                    table.ForeignKey(
                        name: "FK_JSuTarafJawatanSuPekerja_JSuTarafJawatan_JSuTarafJawatanID",
                        column: x => x.JSuTarafJawatanID,
                        principalTable: "JSuTarafJawatan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JSuTarafJawatanSuPekerja_SuPekerja_SuPekerjaId",
                        column: x => x.SuPekerjaId,
                        principalTable: "SuPekerja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JSuTarafJawatanSuPekerja_SuPekerjaId",
                table: "JSuTarafJawatanSuPekerja",
                column: "SuPekerjaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JSuTarafJawatanSuPekerja");

            migrationBuilder.DropColumn(
                name: "JSuTarafJawatanId",
                table: "SuPekerja");
        }
    }
}
