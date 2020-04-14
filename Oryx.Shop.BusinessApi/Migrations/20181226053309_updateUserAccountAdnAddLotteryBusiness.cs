using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateUserAccountAdnAddLotteryBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CustomerCoupon_CustomerAccount_CustomerUserCode",
            //    table: "CustomerCoupon");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_CustomerInfo_CustomerAccount_CustomerUserCode",
            //    table: "CustomerInfo");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_CustomerYanzhi_CustomerAccount_CustomerAccountUserCode",
            //    table: "CustomerYanzhi");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_GoodComments_CustomerAccount_CustomerAccountUserCode",
            //    table: "GoodComments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderEntity_CustomerAccount_CustomerAccountUserCode",
            //    table: "OrderEntity");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PayLogEntity_CustomerAccount_CustomerUserCode",
            //    table: "PayLogEntity");



            //migrationBuilder.DropForeignKey(
            //    name: "FK_ShoppingBasketEntity_CustomerAccount_CustomerUserCode",
            //    table: "ShoppingBasketEntity");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerUserCode",
            //    table: "ShoppingBasketEntity",
            //    newName: "CustomerId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_ShoppingBasketEntity_CustomerUserCode",
            //    table: "ShoppingBasketEntity",
            //    newName: "IX_ShoppingBasketEntity_CustomerId");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerUserCode",
            //    table: "PayLogEntity",
            //    newName: "CustomerId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_PayLogEntity_CustomerUserCode",
            //    table: "PayLogEntity",
            //    newName: "IX_PayLogEntity_CustomerId");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerAccountUserCode",
            //    table: "OrderEntity",
            //    newName: "CustomerAccountId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrderEntity_CustomerAccountUserCode",
            //    table: "OrderEntity",
            //    newName: "IX_OrderEntity_CustomerAccountId");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerAccountUserCode",
            //    table: "GoodComments",
            //    newName: "CustomerAccountId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_GoodComments_CustomerAccountUserCode",
            //    table: "GoodComments",
            //    newName: "IX_GoodComments_CustomerAccountId");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerAccountUserCode",
            //    table: "CustomerYanzhi",
            //    newName: "CustomerAccountId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_CustomerYanzhi_CustomerAccountUserCode",
            //    table: "CustomerYanzhi",
            //    newName: "IX_CustomerYanzhi_CustomerAccountId");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerUserCode",
            //    table: "CustomerInfo",
            //    newName: "CustomerId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_CustomerInfo_CustomerUserCode",
            //    table: "CustomerInfo",
            //    newName: "IX_CustomerInfo_CustomerId");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerUserCode",
            //    table: "CustomerCoupon",
            //    newName: "CustomerId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_CustomerCoupon_CustomerUserCode",
            //    table: "CustomerCoupon",
            //    newName: "IX_CustomerCoupon_CustomerId");

            //migrationBuilder.RenameColumn(
            //    name: "UserCode",
            //    table: "CustomerAccount",
            //    newName: "Id");

            //migrationBuilder.AddColumn<string>(
            //    name: "TrueName",
            //    table: "UserAccountProfile",
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "CustomerAccount",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateTable(
            //    name: "UserAccountBusinessRole",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        RoleName = table.Column<string>(nullable: true),
            //        UserAccountId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserAccountBusinessRole", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserAccountBusinessRole_UserAccountEntry_UserAccountId",
            //            column: x => x.UserAccountId,
            //            principalTable: "UserAccountEntry",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_CustomerAccount_UserAccountId",
            //    table: "CustomerAccount",
            //    column: "UserAccountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAccountBusinessRole_UserAccountId",
            //    table: "UserAccountBusinessRole",
            //    column: "UserAccountId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CustomerAccount_UserAccountEntry_UserAccountId",
            //    table: "CustomerAccount",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CustomerCoupon_CustomerAccount_CustomerId",
            //    table: "CustomerCoupon",
            //    column: "CustomerId",
            //    principalTable: "CustomerAccount",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CustomerInfo_CustomerAccount_CustomerId",
            //    table: "CustomerInfo",
            //    column: "CustomerId",
            //    principalTable: "CustomerAccount",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CustomerYanzhi_CustomerAccount_CustomerAccountId",
            //    table: "CustomerYanzhi",
            //    column: "CustomerAccountId",
            //    principalTable: "CustomerAccount",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_GoodComments_CustomerAccount_CustomerAccountId",
            //    table: "GoodComments",
            //    column: "CustomerAccountId",
            //    principalTable: "CustomerAccount",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderEntity_CustomerAccount_CustomerAccountId",
            //    table: "OrderEntity",
            //    column: "CustomerAccountId",
            //    principalTable: "CustomerAccount",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayLogEntity_CustomerAccount_CustomerId",
                table: "PayLogEntity",
                column: "CustomerId",
                principalTable: "CustomerAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBasketEntity_CustomerAccount_CustomerId",
                table: "ShoppingBasketEntity",
                column: "CustomerId",
                principalTable: "CustomerAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccount_UserAccountEntry_UserAccountId",
                table: "CustomerAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCoupon_CustomerAccount_CustomerId",
                table: "CustomerCoupon");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfo_CustomerAccount_CustomerId",
                table: "CustomerInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerYanzhi_CustomerAccount_CustomerAccountId",
                table: "CustomerYanzhi");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodComments_CustomerAccount_CustomerAccountId",
                table: "GoodComments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_CustomerAccount_CustomerAccountId",
                table: "OrderEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PayLogEntity_CustomerAccount_CustomerId",
                table: "PayLogEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBasketEntity_CustomerAccount_CustomerId",
                table: "ShoppingBasketEntity");

            migrationBuilder.DropTable(
                name: "UserAccountBusinessRole");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccount_UserAccountId",
                table: "CustomerAccount");

            migrationBuilder.DropColumn(
                name: "TrueName",
                table: "UserAccountProfile");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "CustomerAccount");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ShoppingBasketEntity",
                newName: "CustomerUserCode");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingBasketEntity_CustomerId",
                table: "ShoppingBasketEntity",
                newName: "IX_ShoppingBasketEntity_CustomerUserCode");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "PayLogEntity",
                newName: "CustomerUserCode");

            migrationBuilder.RenameIndex(
                name: "IX_PayLogEntity_CustomerId",
                table: "PayLogEntity",
                newName: "IX_PayLogEntity_CustomerUserCode");

            migrationBuilder.RenameColumn(
                name: "CustomerAccountId",
                table: "OrderEntity",
                newName: "CustomerAccountUserCode");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntity_CustomerAccountId",
                table: "OrderEntity",
                newName: "IX_OrderEntity_CustomerAccountUserCode");

            migrationBuilder.RenameColumn(
                name: "CustomerAccountId",
                table: "GoodComments",
                newName: "CustomerAccountUserCode");

            migrationBuilder.RenameIndex(
                name: "IX_GoodComments_CustomerAccountId",
                table: "GoodComments",
                newName: "IX_GoodComments_CustomerAccountUserCode");

            migrationBuilder.RenameColumn(
                name: "CustomerAccountId",
                table: "CustomerYanzhi",
                newName: "CustomerAccountUserCode");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerYanzhi_CustomerAccountId",
                table: "CustomerYanzhi",
                newName: "IX_CustomerYanzhi_CustomerAccountUserCode");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerInfo",
                newName: "CustomerUserCode");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerInfo_CustomerId",
                table: "CustomerInfo",
                newName: "IX_CustomerInfo_CustomerUserCode");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerCoupon",
                newName: "CustomerUserCode");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerCoupon_CustomerId",
                table: "CustomerCoupon",
                newName: "IX_CustomerCoupon_CustomerUserCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CustomerAccount",
                newName: "UserCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCoupon_CustomerAccount_CustomerUserCode",
                table: "CustomerCoupon",
                column: "CustomerUserCode",
                principalTable: "CustomerAccount",
                principalColumn: "UserCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfo_CustomerAccount_CustomerUserCode",
                table: "CustomerInfo",
                column: "CustomerUserCode",
                principalTable: "CustomerAccount",
                principalColumn: "UserCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerYanzhi_CustomerAccount_CustomerAccountUserCode",
                table: "CustomerYanzhi",
                column: "CustomerAccountUserCode",
                principalTable: "CustomerAccount",
                principalColumn: "UserCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodComments_CustomerAccount_CustomerAccountUserCode",
                table: "GoodComments",
                column: "CustomerAccountUserCode",
                principalTable: "CustomerAccount",
                principalColumn: "UserCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntity_CustomerAccount_CustomerAccountUserCode",
                table: "OrderEntity",
                column: "CustomerAccountUserCode",
                principalTable: "CustomerAccount",
                principalColumn: "UserCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayLogEntity_CustomerAccount_CustomerUserCode",
                table: "PayLogEntity",
                column: "CustomerUserCode",
                principalTable: "CustomerAccount",
                principalColumn: "UserCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBasketEntity_CustomerAccount_CustomerUserCode",
                table: "ShoppingBasketEntity",
                column: "CustomerUserCode",
                principalTable: "CustomerAccount",
                principalColumn: "UserCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
