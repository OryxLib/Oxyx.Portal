using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updatePostEntryAddSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDelete",
                table: "PostEntry",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PostEntryCommentLikedLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    PostEntryCommentId = table.Column<Guid>(nullable: false),
                    PostEntryId = table.Column<Guid>(nullable: false),
                    UserAccountEntryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntryCommentLikedLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntryCommentLikedLog_PostEntryComments_PostEntryCommentId",
                        column: x => x.PostEntryCommentId,
                        principalTable: "PostEntryComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEntryCommentLikedLog_PostEntry_PostEntryId",
                        column: x => x.PostEntryId,
                        principalTable: "PostEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEntryCommentLikedLog_UserAccountEntry_UserAccountEntryId",
                        column: x => x.UserAccountEntryId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryCommentLikedLog_PostEntryCommentId",
                table: "PostEntryCommentLikedLog",
                column: "PostEntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryCommentLikedLog_PostEntryId",
                table: "PostEntryCommentLikedLog",
                column: "PostEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryCommentLikedLog_UserAccountEntryId",
                table: "PostEntryCommentLikedLog",
                column: "UserAccountEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostEntryCommentLikedLog");

            migrationBuilder.DropColumn(
                name: "IsSoftDelete",
                table: "PostEntry");
        }
    }
}
