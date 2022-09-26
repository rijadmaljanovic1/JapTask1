using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAP_Management.Infrastructure.Migrations
{
    public partial class addedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentStatus");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "StudentStatus");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Selections");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Mentors");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Selections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SelectionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionStatus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectionStatus");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Selections");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Technologies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Technologies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Technologies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "StudentStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Selections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Mentors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Mentors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Mentors",
                type: "datetime2",
                nullable: true);
        }
    }
}
