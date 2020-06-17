using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meowv.Blog.Migrations
{
    public partial class SecondMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Markdown",
                table: "meowv_Posts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Html",
                table: "meowv_Posts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "meowv_WallPaper",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(maxLength: 20, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meowv_WallPaper", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meowv_WallPaper");

            migrationBuilder.AlterColumn<string>(
                name: "Markdown",
                table: "meowv_Posts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Html",
                table: "meowv_Posts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
