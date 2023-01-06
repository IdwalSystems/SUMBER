using Microsoft.EntityFrameworkCore.Migrations;

namespace SUMBER.Migrations
{
    public partial class addModuleAbWaran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tandatangan",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JBahagianId",
                table: "AbWaran1",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JBahagianId",
                table: "AbWaran",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FlJenisPindahan",
                table: "AbWaran",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AbWaran1_JBahagianId",
                table: "AbWaran1",
                column: "JBahagianId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbWaran1_JBahagian_JBahagianId",
                table: "AbWaran1",
                column: "JBahagianId",
                principalTable: "JBahagian",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbWaran1_JBahagian_JBahagianId",
                table: "AbWaran1");

            migrationBuilder.DropIndex(
                name: "IX_AbWaran1_JBahagianId",
                table: "AbWaran1");

            migrationBuilder.DropColumn(
                name: "Tandatangan",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JBahagianId",
                table: "AbWaran1");

            migrationBuilder.DropColumn(
                name: "FlJenisPindahan",
                table: "AbWaran");

            migrationBuilder.AlterColumn<int>(
                name: "JBahagianId",
                table: "AbWaran",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
