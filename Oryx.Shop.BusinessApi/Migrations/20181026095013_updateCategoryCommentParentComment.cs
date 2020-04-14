using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateCategoryCommentParentComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComment_CategoryComment_ParentCommentId",
                table: "CategoryComment");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentCommentId",
                table: "CategoryComment",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComment_CategoryComment_ParentCommentId",
                table: "CategoryComment",
                column: "ParentCommentId",
                principalTable: "CategoryComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComment_CategoryComment_ParentCommentId",
                table: "CategoryComment");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentCommentId",
                table: "CategoryComment",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComment_CategoryComment_ParentCommentId",
                table: "CategoryComment",
                column: "ParentCommentId",
                principalTable: "CategoryComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
