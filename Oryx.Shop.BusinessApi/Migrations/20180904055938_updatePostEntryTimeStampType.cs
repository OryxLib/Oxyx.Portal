using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updatePostEntryTimeStampType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TimeStamp",
                table: "PostEntry",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "ContentUserReadLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    ContentEntryId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentUserReadLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentUserReadLog_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentUserReadLog_ContentEntry_ContentEntryId",
                        column: x => x.ContentEntryId,
                        principalTable: "ContentEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentUserReadLog_UserAccountEntry_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentUserReadLog_CategoryId",
                table: "ContentUserReadLog",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentUserReadLog_ContentEntryId",
                table: "ContentUserReadLog",
                column: "ContentEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentUserReadLog_UserId",
                table: "ContentUserReadLog",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentUserReadLog");

            migrationBuilder.AlterColumn<int>(
                name: "TimeStamp",
                table: "PostEntry",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
