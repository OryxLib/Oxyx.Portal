using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class addPayApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpenId",
                table: "UserAccountWeChat",
                newName: "UnionId");

            migrationBuilder.CreateTable(
                name: "PayAPILog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    PayNum = table.Column<int>(nullable: false),
                    Statue = table.Column<int>(nullable: false),
                    UserAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayAPILog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayAPILog_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeChatAccountOpenIdMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OpenId = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UnionId = table.Column<string>(nullable: true),
                    UserAccountWechatId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeChatAccountOpenIdMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeChatAccountOpenIdMapping_UserAccountWeChat_UserAccountWechatId",
                        column: x => x.UserAccountWechatId,
                        principalTable: "UserAccountWeChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayAPILog_UserAccountId",
                table: "PayAPILog",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WeChatAccountOpenIdMapping_UserAccountWechatId",
                table: "WeChatAccountOpenIdMapping",
                column: "UserAccountWechatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayAPILog");

            migrationBuilder.DropTable(
                name: "WeChatAccountOpenIdMapping");

            migrationBuilder.RenameColumn(
                name: "UnionId",
                table: "UserAccountWeChat",
                newName: "OpenId");
        }
    }
}
