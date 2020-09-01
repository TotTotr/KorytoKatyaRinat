using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ff1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestName",
                table: "Requests",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestName",
                table: "Requests");
        }
    }
}
