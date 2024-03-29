﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateChatMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChatMessage_UserAccountEntry_FromUserId",
            //    table: "ChatMessage");

            //migrationBuilder.DropIndex(
            //    name: "IX_ChatMessage_FromUserId",
            //    table: "ChatMessage");

            //migrationBuilder.DropColumn(
            //    name: "FromUserId",
            //    table: "ChatMessage");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ChatCollection_UserId",
            //    table: "ChatCollection",
            //    column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatCollection_UserAccountEntry_UserId",
                table: "ChatCollection",
                column: "UserId",
                principalTable: "UserAccountEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatCollection_UserAccountEntry_UserId",
                table: "ChatCollection");

            migrationBuilder.DropIndex(
                name: "IX_ChatCollection_UserId",
                table: "ChatCollection");

            migrationBuilder.AddColumn<Guid>(
                name: "FromUserId",
                table: "ChatMessage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_FromUserId",
                table: "ChatMessage",
                column: "FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_UserAccountEntry_FromUserId",
                table: "ChatMessage",
                column: "FromUserId",
                principalTable: "UserAccountEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
