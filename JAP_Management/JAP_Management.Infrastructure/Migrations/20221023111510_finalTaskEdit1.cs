using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAP_Management.Infrastructure.Migrations
{
    public partial class finalTaskEdit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsProgramItems_Items_ItemId",
                table: "ItemsProgramItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsProgramItems_Programs_ProgramId",
                table: "ItemsProgramItems");

            migrationBuilder.DropTable(
                name: "ItemProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsProgramItems",
                table: "ItemsProgramItems");

            migrationBuilder.RenameTable(
                name: "ItemsProgramItems",
                newName: "ProgramItems");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsProgramItems_ProgramId",
                table: "ProgramItems",
                newName: "IX_ProgramItems_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsProgramItems_ItemId",
                table: "ProgramItems",
                newName: "IX_ProgramItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramItems",
                table: "ProgramItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramItems_Items_ItemId",
                table: "ProgramItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramItems_Programs_ProgramId",
                table: "ProgramItems",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramItems_Items_ItemId",
                table: "ProgramItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramItems_Programs_ProgramId",
                table: "ProgramItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramItems",
                table: "ProgramItems");

            migrationBuilder.RenameTable(
                name: "ProgramItems",
                newName: "ItemsProgramItems");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramItems_ProgramId",
                table: "ItemsProgramItems",
                newName: "IX_ItemsProgramItems_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramItems_ItemId",
                table: "ItemsProgramItems",
                newName: "IX_ItemsProgramItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsProgramItems",
                table: "ItemsProgramItems",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsProgramItems_Items_ItemId",
                table: "ItemsProgramItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsProgramItems_Programs_ProgramId",
                table: "ItemsProgramItems",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
