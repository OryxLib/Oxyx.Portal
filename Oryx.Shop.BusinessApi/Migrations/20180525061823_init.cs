using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUesrEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUesrEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccount",
                columns: table => new
                {
                    UserCode = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    NickName = table.Column<string>(nullable: true),
                    OpenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccount", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "GoodCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodOffer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndScore = table.Column<double>(nullable: false),
                    OfferValue = table.Column<double>(nullable: false),
                    StartScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodOffer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderExpressInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerAddress = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExpressInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerUserCode = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInfo_CustomerAccount_CustomerUserCode",
                        column: x => x.CustomerUserCode,
                        principalTable: "CustomerAccount",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerYanzhi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerAccountUserCode = table.Column<Guid>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerYanzhi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerYanzhi_CustomerAccount_CustomerAccountUserCode",
                        column: x => x.CustomerAccountUserCode,
                        principalTable: "CustomerAccount",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerAccountUserCode = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodComments_CustomerAccount_CustomerAccountUserCode",
                        column: x => x.CustomerAccountUserCode,
                        principalTable: "CustomerAccount",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    CoverPics = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    GoodOfferId = table.Column<Guid>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodEntity_GoodCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "GoodCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodEntity_GoodOffer_GoodOfferId",
                        column: x => x.GoodOfferId,
                        principalTable: "GoodOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerAccountUserCode = table.Column<Guid>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OrderExpressInfoId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserCoder = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEntity_CustomerAccount_CustomerAccountUserCode",
                        column: x => x.CustomerAccountUserCode,
                        principalTable: "CustomerAccount",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderEntity_OrderExpressInfo_OrderExpressInfoId",
                        column: x => x.OrderExpressInfoId,
                        principalTable: "OrderExpressInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingBasketEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerUserCode = table.Column<Guid>(nullable: true),
                    GoodId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBasketEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingBasketEntity_CustomerAccount_CustomerUserCode",
                        column: x => x.CustomerUserCode,
                        principalTable: "CustomerAccount",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingBasketEntity_GoodEntity_GoodId",
                        column: x => x.GoodId,
                        principalTable: "GoodEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderGoodMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    GoodId = table.Column<Guid>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGoodMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderGoodMapping_GoodEntity_GoodId",
                        column: x => x.GoodId,
                        principalTable: "GoodEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderGoodMapping_OrderEntity_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayLogEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerUserCode = table.Column<Guid>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: true),
                    ProcessResult = table.Column<string>(nullable: true),
                    TotalFee = table.Column<int>(nullable: false),
                    TradeNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayLogEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayLogEntity_CustomerAccount_CustomerUserCode",
                        column: x => x.CustomerUserCode,
                        principalTable: "CustomerAccount",
                        principalColumn: "UserCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayLogEntity_OrderEntity_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfo_CustomerUserCode",
                table: "CustomerInfo",
                column: "CustomerUserCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerYanzhi_CustomerAccountUserCode",
                table: "CustomerYanzhi",
                column: "CustomerAccountUserCode");

            migrationBuilder.CreateIndex(
                name: "IX_GoodComments_CustomerAccountUserCode",
                table: "GoodComments",
                column: "CustomerAccountUserCode");

            migrationBuilder.CreateIndex(
                name: "IX_GoodEntity_CategoryId",
                table: "GoodEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodEntity_GoodOfferId",
                table: "GoodEntity",
                column: "GoodOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_CustomerAccountUserCode",
                table: "OrderEntity",
                column: "CustomerAccountUserCode");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_OrderExpressInfoId",
                table: "OrderEntity",
                column: "OrderExpressInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderGoodMapping_GoodId",
                table: "OrderGoodMapping",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGoodMapping_OrderId",
                table: "OrderGoodMapping",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PayLogEntity_CustomerUserCode",
                table: "PayLogEntity",
                column: "CustomerUserCode");

            migrationBuilder.CreateIndex(
                name: "IX_PayLogEntity_OrderId",
                table: "PayLogEntity",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBasketEntity_CustomerUserCode",
                table: "ShoppingBasketEntity",
                column: "CustomerUserCode");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBasketEntity_GoodId",
                table: "ShoppingBasketEntity",
                column: "GoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUesrEntity");

            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropTable(
                name: "CustomerYanzhi");

            migrationBuilder.DropTable(
                name: "GoodComments");

            migrationBuilder.DropTable(
                name: "OrderGoodMapping");

            migrationBuilder.DropTable(
                name: "PayLogEntity");

            migrationBuilder.DropTable(
                name: "ShoppingBasketEntity");

            migrationBuilder.DropTable(
                name: "OrderEntity");

            migrationBuilder.DropTable(
                name: "GoodEntity");

            migrationBuilder.DropTable(
                name: "CustomerAccount");

            migrationBuilder.DropTable(
                name: "OrderExpressInfo");

            migrationBuilder.DropTable(
                name: "GoodCategory");

            migrationBuilder.DropTable(
                name: "GoodOffer");
        }
    }
}
