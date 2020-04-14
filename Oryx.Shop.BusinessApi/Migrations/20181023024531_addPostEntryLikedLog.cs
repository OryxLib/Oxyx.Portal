using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class addPostEntryLikedLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostEntryLikedLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    PostEntryId = table.Column<Guid>(nullable: false),
                    UserAccountEntryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntryLikedLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntryLikedLog_PostEntry_PostEntryId",
                        column: x => x.PostEntryId,
                        principalTable: "PostEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEntryLikedLog_UserAccountEntry_UserAccountEntryId",
                        column: x => x.UserAccountEntryId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryLikedLog_PostEntryId",
                table: "PostEntryLikedLog",
                column: "PostEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryLikedLog_UserAccountEntryId",
                table: "PostEntryLikedLog",
                column: "UserAccountEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostEntryLikedLog");
        }
    }
}
