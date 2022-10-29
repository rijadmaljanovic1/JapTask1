using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAP_Management.Infrastructure.Migrations
{
    public partial class finalEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "SelectionItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "SelectionItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_StudentItems_ItemId",
                table: "StudentItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionItems_ItemId",
                table: "SelectionItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectionItems_Items_ItemId",
                table: "SelectionItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentItems_Items_ItemId",
                table: "StudentItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectionItems_Items_ItemId",
                table: "SelectionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentItems_Items_ItemId",
                table: "StudentItems");

            migrationBuilder.DropIndex(
                name: "IX_StudentItems_ItemId",
                table: "StudentItems");

            migrationBuilder.DropIndex(
                name: "IX_SelectionItems_ItemId",
                table: "SelectionItems");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "SelectionItems");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "SelectionItems");
        }
    }
}
