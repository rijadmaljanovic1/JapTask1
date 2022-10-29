using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAP_Management.Infrastructure.Migrations
{
    public partial class finalTaskEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Programs_ProgramId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ProgramId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "ItemProgram",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "int", nullable: false),
                    ProgramsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProgram", x => new { x.ItemsId, x.ProgramsId });
                    table.ForeignKey(
                        name: "FK_ItemProgram_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemProgram_Programs_ProgramsId",
                        column: x => x.ProgramsId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemProgram_ProgramsId",
                table: "ItemProgram",
                column: "ProgramsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemProgram");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProgramId",
                table: "Items",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Programs_ProgramId",
                table: "Items",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id");
        }
    }
}
