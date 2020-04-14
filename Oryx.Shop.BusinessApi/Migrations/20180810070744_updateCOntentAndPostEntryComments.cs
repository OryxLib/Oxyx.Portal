using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateCOntentAndPostEntryComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentEntry_Comment_CommentsId",
                table: "ContentEntry");

            migrationBuilder.DropIndex(
                name: "IX_ContentEntry_CommentsId",
                table: "ContentEntry");

            migrationBuilder.DropColumn(
                name: "CommentsId",
                table: "ContentEntry");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentEntryId",
                table: "Comment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ContentEntryId",
                table: "Comment",
                column: "ContentEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ContentEntry_ContentEntryId",
                table: "Comment",
                column: "ContentEntryId",
                principalTable: "ContentEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ContentEntry_ContentEntryId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ContentEntryId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ContentEntryId",
                table: "Comment");

            migrationBuilder.AddColumn<Guid>(
                name: "CommentsId",
                table: "ContentEntry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntry_CommentsId",
                table: "ContentEntry",
                column: "CommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentEntry_Comment_CommentsId",
                table: "ContentEntry",
                column: "CommentsId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
