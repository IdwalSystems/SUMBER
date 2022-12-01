using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class addTableJSuKodGaji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkPVGanda_JBank_JBankId",
                table: "AkPVGanda");

            migrationBuilder.AlterColumn<int>(
                name: "JCaraBayarId",
                table: "AkPVGanda",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "JBankId",
                table: "AkPVGanda",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "JSuKodGaji",
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
                    table.PrimaryKey("PK_JSuKodGaji", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AkPVGanda_JBank_JBankId",
                table: "AkPVGanda",
                column: "JBankId",
                principalTable: "JBank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkPVGanda_JBank_JBankId",
                table: "AkPVGanda");

            migrationBuilder.DropTable(
                name: "JSuKodGaji");

            migrationBuilder.AlterColumn<int>(
                name: "JCaraBayarId",
                table: "AkPVGanda",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JBankId",
                table: "AkPVGanda",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkPVGanda_JBank_JBankId",
                table: "AkPVGanda",
                column: "JBankId",
                principalTable: "JBank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
