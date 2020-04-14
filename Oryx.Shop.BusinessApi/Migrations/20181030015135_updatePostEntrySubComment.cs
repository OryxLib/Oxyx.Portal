using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updatePostEntrySubComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                table: "PostEntryComments");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                table: "PostEntryComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentCommentId",
                table: "PostEntryComments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_PostEntryComments_ParentCommentId",
                table: "PostEntryComments",
                column: "ParentCommentId",
                principalTable: "PostEntryComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
