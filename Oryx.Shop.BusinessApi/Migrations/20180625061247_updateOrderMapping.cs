using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateOrderMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "OrderGoodMapping",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderEntityId",
                table: "CouponEntity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponEntity_OrderEntityId",
                table: "CouponEntity",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CouponEntity_OrderEntity_OrderEntityId",
                table: "CouponEntity",
                column: "OrderEntityId",
                principalTable: "OrderEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CouponEntity_OrderEntity_OrderEntityId",
                table: "CouponEntity");

            migrationBuilder.DropIndex(
                name: "IX_CouponEntity_OrderEntityId",
                table: "CouponEntity");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "OrderGoodMapping");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "CouponEntity");
        }
    }
}
