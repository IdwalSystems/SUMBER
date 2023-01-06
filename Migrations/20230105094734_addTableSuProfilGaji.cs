using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class addTableSuProfilGaji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuProfilGaji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuPekerjaId = table.Column<int>(type: "int", nullable: false),
                    JSuKodGajiId = table.Column<int>(type: "int", nullable: false),
                    Elaun = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Potongan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlKWSP = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SuProfilGaji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuProfilGaji_JSuKodGaji_JSuKodGajiId",
                        column: x => x.JSuKodGajiId,
                        principalTable: "JSuKodGaji",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuProfilGaji_SuPekerja_SuPekerjaId",
                        column: x => x.SuPekerjaId,
                        principalTable: "SuPekerja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuProfilGaji_JSuKodGajiId",
                table: "SuProfilGaji",
                column: "JSuKodGajiId");

            migrationBuilder.CreateIndex(
                name: "IX_SuProfilGaji_SuPekerjaId",
                table: "SuProfilGaji",
                column: "SuPekerjaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuProfilGaji");
        }
    }
}
