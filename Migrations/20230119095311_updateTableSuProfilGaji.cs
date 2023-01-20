using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class updateTableSuProfilGaji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlHapus",
                table: "SuProfilGaji");

            migrationBuilder.DropColumn(
                name: "SuPekerjaKemaskiniId",
                table: "SuProfilGaji");

            migrationBuilder.DropColumn(
                name: "SuPekerjaMasukId",
                table: "SuProfilGaji");

            migrationBuilder.DropColumn(
                name: "TarHapus",
                table: "SuProfilGaji");

            migrationBuilder.DropColumn(
                name: "TarKemaskini",
                table: "SuProfilGaji");

            migrationBuilder.DropColumn(
                name: "TarMasuk",
                table: "SuProfilGaji");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SuProfilGaji");

            migrationBuilder.DropColumn(
                name: "UserIdKemaskini",
                table: "SuProfilGaji");

            migrationBuilder.AlterColumn<string>(
                name: "Kod",
                table: "JSuKodGaji",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<int>(
                name: "FlHapus",
                table: "SuProfilGaji",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuPekerjaKemaskiniId",
                table: "SuProfilGaji",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuPekerjaMasukId",
                table: "SuProfilGaji",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TarHapus",
                table: "SuProfilGaji",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TarKemaskini",
                table: "SuProfilGaji",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TarMasuk",
                table: "SuProfilGaji",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SuProfilGaji",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdKemaskini",
                table: "SuProfilGaji",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Kod",
                table: "JSuKodGaji",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

        }
    }
}
