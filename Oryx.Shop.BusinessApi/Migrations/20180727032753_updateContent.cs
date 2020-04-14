using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "UserAccountEntry",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserNamePwdId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CostRewardPoints",
                table: "ContentPayLog",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "CostMoney",
                table: "ContentPayLog",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "PayContentId",
                table: "ContentPayLog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayContentType",
                table: "ContentPayLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserAccountId",
                table: "ContentPayLog",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Fee",
                table: "ContentEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fee",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoriesId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountUserNamePwd",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountUserNamePwd", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_UserNamePwdId",
                table: "UserAccountEntry",
                column: "UserNamePwdId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CategoriesId",
                table: "Tags",
                column: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_UserAccountUserNamePwd_UserNamePwdId",
                table: "UserAccountEntry",
                column: "UserNamePwdId",
                principalTable: "UserAccountUserNamePwd",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountEntry_UserAccountUserNamePwd_UserNamePwdId",
                table: "UserAccountEntry");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "UserAccountUserNamePwd");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountEntry_UserNamePwdId",
                table: "UserAccountEntry");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "UserAccountEntry");

            migrationBuilder.DropColumn(
                name: "UserNamePwdId",
                table: "UserAccountEntry");

            migrationBuilder.DropColumn(
                name: "PayContentId",
                table: "ContentPayLog");

            migrationBuilder.DropColumn(
                name: "PayContentType",
                table: "ContentPayLog");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "ContentPayLog");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "ContentEntry");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categories");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostRewardPoints",
                table: "ContentPayLog",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "CostMoney",
                table: "ContentPayLog",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
