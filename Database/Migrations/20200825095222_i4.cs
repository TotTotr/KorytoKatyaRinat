using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class i4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Details_DetailId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_DetailId",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "DetailRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(nullable: false),
                    DetailId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailRequests_Details_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailRequests_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailRequests_DetailId",
                table: "DetailRequests",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailRequests_RequestId",
                table: "DetailRequests",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailRequests");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DetailId",
                table: "Requests",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Details_DetailId",
                table: "Requests",
                column: "DetailId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
