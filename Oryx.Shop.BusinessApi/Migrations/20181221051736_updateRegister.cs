using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "PayAPILog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayAPIOrderId",
                table: "PayAPILog",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAccountInviteOrign",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InviteKey = table.Column<string>(nullable: true),
                    UserAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountInviteOrign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccountInviteOrign_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountInviteOrign_UserAccountId",
                table: "UserAccountInviteOrign",
                column: "UserAccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccountInviteOrign");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "PayAPILog");

            migrationBuilder.DropColumn(
                name: "PayAPIOrderId",
                table: "PayAPILog");
        }
    }
}
