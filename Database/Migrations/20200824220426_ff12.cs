using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ff12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Admins_AdminId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Admins_AdminId",
                table: "Requests",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Admins_AdminId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Admins_AdminId",
                table: "Requests",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
