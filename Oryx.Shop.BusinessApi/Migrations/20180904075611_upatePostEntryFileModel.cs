using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class upatePostEntryFileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PostEntryFile",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TimeStamp",
                table: "ChatMessage",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PostEntryFile");

            migrationBuilder.AlterColumn<int>(
                name: "TimeStamp",
                table: "ChatMessage",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
