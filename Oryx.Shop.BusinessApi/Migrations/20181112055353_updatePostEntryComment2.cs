using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updatePostEntryComment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostEntryId",
                table: "PostEntryComments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments",
                column: "PostEntryId",
                principalTable: "PostEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostEntryId",
                table: "PostEntryComments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntryComments_PostEntry_PostEntryId",
                table: "PostEntryComments",
                column: "PostEntryId",
                principalTable: "PostEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
