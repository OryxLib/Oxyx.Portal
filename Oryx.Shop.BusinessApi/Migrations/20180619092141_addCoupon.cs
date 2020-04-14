using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class addCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoodsType",
                table: "GoodEntity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CouponEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<string>(nullable: true),
                    CouponType = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    GoodsType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCoupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CanUse = table.Column<bool>(nullable: false),
                    CouponMappingId = table.Column<Guid>(nullable: true),
                    CustomerUserCode = table.Column<Guid>(nullable: true),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCoupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCoupon_CouponEntity_CouponMappingId",
                        column: x => x.CouponMappingId,
                        principalTable: "CouponEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerCoupon_CustomerAccount_CustomerUserCode",
                        column: x => x.CustomerUserCode,
                        principalTable: "CustomerAccount",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCoupon_CouponMappingId",
                table: "CustomerCoupon",
                column: "CouponMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCoupon_CustomerUserCode",
                table: "CustomerCoupon",
                column: "CustomerUserCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCoupon");

            migrationBuilder.DropTable(
                name: "CouponEntity");

            migrationBuilder.DropColumn(
                name: "GoodsType",
                table: "GoodEntity");
        }
    }
}
