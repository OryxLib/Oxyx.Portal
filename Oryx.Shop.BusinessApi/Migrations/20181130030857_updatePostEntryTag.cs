using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updatePostEntryTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentEntryInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentEntryInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentEntryInfo_ContentEntry_Id",
                        column: x => x.Id,
                        principalTable: "ContentEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentEntryInfo_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostEntryTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PostEntryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntryTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEntryTags_PostEntry_PostEntryId",
                        column: x => x.PostEntryId,
                        principalTable: "PostEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntryInfo_UserAccountId",
                table: "ContentEntryInfo",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntryTags_PostEntryId",
                table: "PostEntryTags",
                column: "PostEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentEntryInfo");

            migrationBuilder.DropTable(
                name: "PostEntryTags");
        }
    }
}
