using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAccountProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountProfile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_ProfileId",
                table: "UserAccountEntry",
                column: "ProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_UserAccountProfile_ProfileId",
                table: "UserAccountEntry",
                column: "ProfileId",
                principalTable: "UserAccountProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountEntry_UserAccountProfile_ProfileId",
                table: "UserAccountEntry");

            migrationBuilder.DropTable(
                name: "UserAccountProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountEntry_ProfileId",
                table: "UserAccountEntry");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "UserAccountEntry");
        }
    }
}
