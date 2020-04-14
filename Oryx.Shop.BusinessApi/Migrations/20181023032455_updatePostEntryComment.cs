using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updatePostEntryComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                table: "PostEntryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_UserAccountEntry_UserAccountId",
                table: "PostEntryComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserAccountId",
                table: "PostEntryComments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TimeStamp",
                table: "PostEntryComments",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "PostEntryId",
                table: "PostEntryComments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentCommentId",
                table: "PostEntryComments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeSum",
                table: "PostEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                table: "PostEntryComments",
                column: "ParentCommentId",
                principalTable: "PostEntryComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments",
                column: "PostEntryId",
                principalTable: "PostEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_UserAccountEntry_UserAccountId",
                table: "PostEntryComments",
                column: "UserAccountId",
                principalTable: "UserAccountEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                table: "PostEntryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_UserAccountEntry_UserAccountId",
                table: "PostEntryComments");

            migrationBuilder.DropColumn(
                name: "LikeSum",
                table: "PostEntry");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserAccountId",
                table: "PostEntryComments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "TimeStamp",
                table: "PostEntryComments",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<Guid>(
                name: "PostEntryId",
                table: "PostEntryComments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentCommentId",
                table: "PostEntryComments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                table: "PostEntryComments",
                column: "ParentCommentId",
                principalTable: "PostEntryComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments",
                column: "PostEntryId",
                principalTable: "PostEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_UserAccountEntry_UserAccountId",
                table: "PostEntryComments",
                column: "UserAccountId",
                principalTable: "UserAccountEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
