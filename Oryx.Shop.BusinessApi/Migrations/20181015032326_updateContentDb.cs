using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateContentDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ToUserId",
                table: "ChatMessage",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ToUserId",
                table: "ChatMessage",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_UserAccountEntry_ToUserId",
                table: "ChatMessage",
                column: "ToUserId",
                principalTable: "UserAccountEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_UserAccountEntry_ToUserId",
                table: "ChatMessage");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessage_ToUserId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "ChatMessage");
        }
    }
}
