using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateCommentWithPostEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ParentCommentId = table.Column<Guid>(nullable: false),
                    UserAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryComment_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryComment_CategoryComment_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "CategoryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryComment_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPostEntryMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    PostEntryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPostEntryMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentExtPostEntryTopic",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LinkId = table.Column<Guid>(nullable: false),
                    LinkType = table.Column<string>(nullable: true),
                    TopicText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentExtPostEntryTopic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentPostEntryMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContentId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    PostEntryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPostEntryMapping", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostEntry_UserId",
                table: "PostEntry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryComment_CategoryId",
                table: "CategoryComment",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryComment_ParentCommentId",
                table: "CategoryComment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryComment_UserAccountId",
                table: "CategoryComment",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntry_UserAccountEntry_UserId",
                table: "PostEntry",
                column: "UserId",
                principalTable: "UserAccountEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntry_UserAccountEntry_UserId",
                table: "PostEntry");

            migrationBuilder.DropTable(
                name: "CategoryComment");

            migrationBuilder.DropTable(
                name: "CategoryPostEntryMapping");

            migrationBuilder.DropTable(
                name: "ContentExtPostEntryTopic");

            migrationBuilder.DropTable(
                name: "ContentPostEntryMapping");

            migrationBuilder.DropIndex(
                name: "IX_PostEntry_UserId",
                table: "PostEntry");
        }
    }
}
