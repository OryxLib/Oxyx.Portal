using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oryx.Spider.StandardV3.Migrations
{
    public partial class initcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpiderLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParentUrl = table.Column<string>(nullable: true),
                    ParentName = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    TargetUrl = table.Column<string>(nullable: true),
                    ReloadSuccess = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Html = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpiderLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpiderLogs");
        }
    }
}
