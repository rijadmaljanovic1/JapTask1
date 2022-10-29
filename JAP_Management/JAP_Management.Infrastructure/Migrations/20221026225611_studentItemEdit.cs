using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAP_Management.Infrastructure.Migrations
{
    public partial class studentItemEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentItems_Students_StudentBaseUserId",
                table: "StudentItems");

            migrationBuilder.DropIndex(
                name: "IX_StudentItems_StudentBaseUserId",
                table: "StudentItems");

            migrationBuilder.DropColumn(
                name: "StudentBaseUserId",
                table: "StudentItems");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_StudentItems_StudentId",
                table: "StudentItems",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentItems_Students_StudentId",
                table: "StudentItems",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "BaseUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentItems_Students_StudentId",
                table: "StudentItems");

            migrationBuilder.DropIndex(
                name: "IX_StudentItems_StudentId",
                table: "StudentItems");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StudentBaseUserId",
                table: "StudentItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StudentItems_StudentBaseUserId",
                table: "StudentItems",
                column: "StudentBaseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentItems_Students_StudentBaseUserId",
                table: "StudentItems",
                column: "StudentBaseUserId",
                principalTable: "Students",
                principalColumn: "BaseUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
