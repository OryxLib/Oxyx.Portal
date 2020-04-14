using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateSocialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    TextContent = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostEntryUserAnchor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NextTimeStamp = table.Column<int>(nullable: false),
                    NextTimeStampOrder = table.Column<int>(nullable: false),
                    PrevTimeStamp = table.Column<int>(nullable: false),
                    PrevTimeStampOrder = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntryUserAnchor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChatLogId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatCollection_ChatLog_ChatLogId",
                        column: x => x.ChatLogId,
                        principalTable: "ChatLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChageLogId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    FromUserId = table.Column<Guid>(nullable: true),
                    IsReaded = table.Column<bool>(nullable: false),
                    MessageContent = table.Column<string>(nullable: true),
                    MsgType = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_ChatLog_ChageLogId",
                        column: x => x.ChageLogId,
                        principalTable: "ChatLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessage_UserAccountEntry_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostEntryComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    LikeNum = table.Column<int>(nullable: false),
                    ParentCommentId = table.Column<Guid>(nullable: true),
                    PostEntryId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<int>(nullable: false),
                    UserAccountId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntryComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "PostEntryComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostEntryComments_PostEntry_PostEntryId",
                        column: x => x.PostEntryId,
                        principalTable: "PostEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostEntryComments_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostEntryFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActualPath = table.Column<string>(nullable: true),
                    PostEntryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntryFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntryFile_PostEntry_PostEntryId",
                        column: x => x.PostEntryId,
                        principalTable: "PostEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostEntrySocialInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LikeNum = table.Column<int>(nullable: false),
                    PostEntryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntrySocialInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntrySocialInfo_PostEntry_PostEntryId",
                        column: x => x.PostEntryId,
                        principalTable: "PostEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatCollection_ChatLogId",
                table: "ChatCollection",
                column: "ChatLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChageLogId",
                table: "ChatMessage",
                column: "ChageLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_FromUserId",
                table: "ChatMessage",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryComments_ParentCommentId",
                table: "PostEntryComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryComments_PostEntryId",
                table: "PostEntryComments",
                column: "PostEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryComments_UserAccountId",
                table: "PostEntryComments",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryFile_PostEntryId",
                table: "PostEntryFile",
                column: "PostEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntrySocialInfo_PostEntryId",
                table: "PostEntrySocialInfo",
                column: "PostEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatCollection");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "PostEntryComments");

            migrationBuilder.DropTable(
                name: "PostEntryFile");

            migrationBuilder.DropTable(
                name: "PostEntrySocialInfo");

            migrationBuilder.DropTable(
                name: "PostEntryUserAnchor");

            migrationBuilder.DropTable(
                name: "ChatLog");

            migrationBuilder.DropTable(
                name: "PostEntry");
        }
    }
}
