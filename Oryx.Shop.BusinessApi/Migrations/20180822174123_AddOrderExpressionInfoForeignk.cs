using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class AddOrderExpressionInfoForeignk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderEntity_OrderExpressInfo_OrderExpressInfoId",
            //    table: "OrderEntity");

            //migrationBuilder.DropIndex(
            //    name: "IX_OrderEntity_OrderExpressInfoId",
            //    table: "OrderEntity");

            //migrationBuilder.DropColumn(
            //    name: "OrderExpressInfoId",
            //    table: "OrderEntity");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "OrderEntityId",
            //    table: "OrderExpressInfo",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderExpressInfo_OrderEntityId",
            //    table: "OrderExpressInfo",
            //    column: "OrderEntityId",
            //    unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderExpressInfo_OrderEntity_OrderEntityId",
            //    table: "OrderExpressInfo",
            //    column: "OrderEntityId",
            //    principalTable: "OrderEntity",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderExpressInfo_OrderEntity_OrderEntityId",
                table: "OrderExpressInfo");

            migrationBuilder.DropIndex(
                name: "IX_OrderExpressInfo_OrderEntityId",
                table: "OrderExpressInfo");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "OrderExpressInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderExpressInfoId",
                table: "OrderEntity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_OrderExpressInfoId",
                table: "OrderEntity",
                column: "OrderExpressInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntity_OrderExpressInfo_OrderExpressInfoId",
                table: "OrderEntity",
                column: "OrderExpressInfoId",
                principalTable: "OrderExpressInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
