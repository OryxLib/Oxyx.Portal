using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class addDynamicPageAddNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReponseEntry_ReponseEntry_MasterId",
                table: "ReponseEntry");

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterId",
                table: "ReponseEntry",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ReponseEntry_ReponseEntry_MasterId",
                table: "ReponseEntry",
                column: "MasterId",
                principalTable: "ReponseEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReponseEntry_ReponseEntry_MasterId",
                table: "ReponseEntry");

            migrationBuilder.AlterColumn<Guid>(
                name: "MasterId",
                table: "ReponseEntry",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReponseEntry_ReponseEntry_MasterId",
                table: "ReponseEntry",
                column: "MasterId",
                principalTable: "ReponseEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
