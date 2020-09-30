using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalMenuApi.Migrations
{
    public partial class RefactorDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Box_Session1",
                table: "Box");

            migrationBuilder.DropForeignKey(
                name: "FK_Box_Session",
                table: "Box");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Box_FooterId",
                table: "Box");

            migrationBuilder.DropIndex(
                name: "IX_Box_HeaderId",
                table: "Box");

            migrationBuilder.DropColumn(
                name: "FooterId",
                table: "Box");

            migrationBuilder.DropColumn(
                name: "HeaderId",
                table: "Box");

            migrationBuilder.AddColumn<string>(
                name: "FooterTitle",
                table: "Box",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterUrl",
                table: "Box",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTitle",
                table: "Box",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderUrl",
                table: "Box",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterTitle",
                table: "Box");

            migrationBuilder.DropColumn(
                name: "FooterUrl",
                table: "Box");

            migrationBuilder.DropColumn(
                name: "HeaderTitle",
                table: "Box");

            migrationBuilder.DropColumn(
                name: "HeaderUrl",
                table: "Box");

            migrationBuilder.AddColumn<int>(
                name: "FooterId",
                table: "Box",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeaderId",
                table: "Box",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Box_FooterId",
                table: "Box",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Box_HeaderId",
                table: "Box",
                column: "HeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Box_Session1",
                table: "Box",
                column: "FooterId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Box_Session",
                table: "Box",
                column: "HeaderId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
