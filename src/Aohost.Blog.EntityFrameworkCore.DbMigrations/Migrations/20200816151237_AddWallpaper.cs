using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aohost.Blog.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class AddWallpaper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aohost_Wallpapers",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 50, nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aohost_Wallpapers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aohost_Wallpapers");
        }
    }
}
