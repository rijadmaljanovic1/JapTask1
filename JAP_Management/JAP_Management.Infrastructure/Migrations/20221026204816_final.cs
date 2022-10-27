using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAP_Management.Infrastructure.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Selections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SelectionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SelectionId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectionItems_Selections_SelectionId",
                        column: x => x.SelectionId,
                        principalTable: "Selections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    PercentageDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentBaseUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentItems_Students_StudentBaseUserId",
                        column: x => x.StudentBaseUserId,
                        principalTable: "Students",
                        principalColumn: "BaseUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectionItems_SelectionId",
                table: "SelectionItems",
                column: "SelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentItems_StudentBaseUserId",
                table: "StudentItems",
                column: "StudentBaseUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectionItems");

            migrationBuilder.DropTable(
                name: "StudentItems");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Selections");
        }
    }
}
