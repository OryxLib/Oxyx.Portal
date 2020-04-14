using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateREponseEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ContentEntry_Categories_CategoryId",
            //    table: "ContentEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_FileEntry_Categories_CategoriesId",
            //    table: "FileEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Tags_Categories_CategoriesId",
            //    table: "Tags");

            //migrationBuilder.DropTable(
            //    name: "ActivityLog");

            //migrationBuilder.DropTable(
            //    name: "AdminUesrEntity");

            //migrationBuilder.DropTable(
            //    name: "CustomerCoupon");

            //migrationBuilder.DropTable(
            //    name: "CustomerInfo");

            //migrationBuilder.DropTable(
            //    name: "CustomerYanzhi");

            //migrationBuilder.DropTable(
            //    name: "GoodComments");

            //migrationBuilder.DropTable(
            //    name: "OrderExpressInfo");

            //migrationBuilder.DropTable(
            //    name: "OrderGoodMapping");

            //migrationBuilder.DropTable(
            //    name: "PayAPILog");

            //migrationBuilder.DropTable(
            //    name: "PayLogEntity");

            //migrationBuilder.DropTable(
            //    name: "ShoppingBasketEntity");

            //migrationBuilder.DropTable(
            //    name: "CouponEntity");

            //migrationBuilder.DropTable(
            //    name: "GoodEntity");

            //migrationBuilder.DropTable(
            //    name: "OrderEntity");

            //migrationBuilder.DropTable(
            //    name: "GoodCategory");

            //migrationBuilder.DropTable(
            //    name: "GoodOffer");

            //migrationBuilder.DropTable(
            //    name: "CustomerAccount");

            //migrationBuilder.RenameColumn(
            //    name: "CategoriesId",
            //    table: "Tags",
            //    newName: "CategoryId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Tags_CategoriesId",
            //    table: "Tags",
            //    newName: "IX_Tags_CategoryId");

            ////migrationBuilder.RenameColumn(
            ////    name: "CategoriesId",
            ////    table: "FileEntry",
            ////    newName: "CategoryId");

            ////migrationBuilder.RenameIndex(
            ////    name: "IX_FileEntry_CategoriesId",
            ////    table: "FileEntry",
            ////    newName: "IX_FileEntry_CategoryId");

            ////migrationBuilder.AddColumn<DateTime>(
            ////    name: "CreateTime",
            ////    table: "UserAccountBusinessRole",
            ////    nullable: false,
            ////    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            ////migrationBuilder.AddColumn<DateTime>(
            ////    name: "CreateTime",
            ////    table: "Tags",
            ////    nullable: false,
            ////    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<string>(
            //    name: "Description",
            //    table: "FileEntry",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Extension",
            //    table: "FileEntry",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Icon",
            //    table: "FileEntry",
            //    nullable: true);

            //////migrationBuilder.AddColumn<string>(
            //////    name: "PhysicalPath",
            //////    table: "FileEntry",
            //////    nullable: true);

            //////migrationBuilder.AlterColumn<Guid>(
            //////    name: "CategoryId",
            //////    table: "ContentEntry",
            //////    nullable: false,
            //////    oldClrType: typeof(Guid),
            //////    oldNullable: true);

            ////migrationBuilder.AddColumn<string>(
            ////    name: "Description",
            ////    table: "ContentEntry",
            ////    nullable: true);

            ////migrationBuilder.AddColumn<bool>(
            ////    name: "IsFaq",
            ////    table: "ContentEntry",
            ////    nullable: false,
            ////    defaultValue: false);

            ////migrationBuilder.AddColumn<bool>(
            ////    name: "IsTop",
            ////    table: "ContentEntry",
            ////    nullable: false,
            ////    defaultValue: false);

            ////migrationBuilder.AddColumn<bool>(
            ////    name: "IsShowTop",
            ////    table: "Categories",
            ////    nullable: false,
            ////    defaultValue: false);

            //////migrationBuilder.CreateTable(
            //////    name: "TipEntry",
            //////    columns: table => new
            //////    {
            //////        Id = table.Column<Guid>(nullable: false),
            //////        Content = table.Column<string>(nullable: true),
            //////        CreateTime = table.Column<DateTime>(nullable: false),
            //////        Link = table.Column<string>(nullable: true),
            //////        PublishTime = table.Column<DateTime>(nullable: true),
            //////        Title = table.Column<string>(nullable: true)
            //////    },
            //////    constraints: table =>
            //////    {
            //////        table.PrimaryKey("PK_TipEntry", x => x.Id);
            //////    });

            ////migrationBuilder.CreateTable(
            ////    name: "UserAccountLoginLogs",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<Guid>(nullable: false),
            ////        CreateTime = table.Column<DateTime>(nullable: false),
            ////        IP = table.Column<string>(nullable: true),
            ////        UserAccountEntryId = table.Column<Guid>(nullable: false)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_UserAccountLoginLogs", x => x.Id);
            ////        table.ForeignKey(
            ////            name: "FK_UserAccountLoginLogs_UserAccountEntry_UserAccountEntryId",
            ////            column: x => x.UserAccountEntryId,
            ////            principalTable: "UserAccountEntry",
            ////            principalColumn: "Id",
            ////            onDelete: ReferentialAction.Cascade);
            ////    });

            ////migrationBuilder.CreateIndex(
            ////    name: "IX_UserAccountLoginLogs_UserAccountEntryId",
            ////    table: "UserAccountLoginLogs",
            ////    column: "UserAccountEntryId");

            ////migrationBuilder.AddForeignKey(
            ////    name: "FK_ContentEntry_Categories_CategoryId",
            ////    table: "ContentEntry",
            ////    column: "CategoryId",
            ////    principalTable: "Categories",
            ////    principalColumn: "Id",
            ////    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_FileEntry_Categories_CategoryId",
            //    table: "FileEntry",
            //    column: "CategoryId",
            //    principalTable: "Categories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            ////////////migrationBuilder.AddForeignKey(
            ////////////    name: "FK_Tags_Categories_CategoryId",
            ////////////    table: "Tags",
            ////////////    column: "CategoryId",
            ////////////    principalTable: "Categories",
            ////////////    principalColumn: "Id",
            ////////////    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ContentEntry_Categories_CategoryId",
            //    table: "ContentEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_FileEntry_Categories_CategoryId",
                table: "FileEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Categories_CategoryId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "TipEntry");

            migrationBuilder.DropTable(
                name: "UserAccountLoginLogs");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "UserAccountBusinessRole");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FileEntry");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "FileEntry");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "FileEntry");

            migrationBuilder.DropColumn(
                name: "PhysicalPath",
                table: "FileEntry");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ContentEntry");

            migrationBuilder.DropColumn(
                name: "IsFaq",
                table: "ContentEntry");

            migrationBuilder.DropColumn(
                name: "IsTop",
                table: "ContentEntry");

            migrationBuilder.DropColumn(
                name: "IsShowTop",
                table: "Categories");

            //migrationBuilder.RenameColumn(
            //    name: "CategoryId",
            //    table: "Tags",
            //    newName: "CategoriesId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Tags_CategoryId",
            //    table: "Tags",
            //    newName: "IX_Tags_CategoriesId");

            //migrationBuilder.RenameColumn(
            //    name: "CategoryId",
            //    table: "FileEntry",
            //    newName: "CategoriesId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_FileEntry_CategoryId",
            //    table: "FileEntry",
            //    newName: "IX_FileEntry_CategoriesId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "ContentEntry",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    UserAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLog_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Id = table.Column<Guid>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    NickName = table.Column<string>(nullable: true),
                    OpenId = table.Column<string>(nullable: true),
                    UserAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAccount_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "PayAPILog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    PayAPIOrderId = table.Column<string>(nullable: true),
                    PayNum = table.Column<int>(nullable: false),
                    Statue = table.Column<int>(nullable: false),
                    UserAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayAPILog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayAPILog_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInfo_CustomerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerYanzhi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerAccountId = table.Column<Guid>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerYanzhi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerYanzhi_CustomerAccount_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerAccountId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodComments_CustomerAccount_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerAccountId = table.Column<Guid>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OutTradeNo = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserCoder = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEntity_CustomerAccount_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
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
                    GoodsType = table.Column<string>(nullable: true),
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
                name: "CouponEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<string>(nullable: true),
                    CouponType = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    GoodsType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OrderEntityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponEntity_OrderEntity_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderExpressInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerAddress = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerPhone = table.Column<string>(nullable: true),
                    OrderEntityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExpressInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderExpressInfo_OrderEntity_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayLogEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: true),
                    ProcessResult = table.Column<string>(nullable: true),
                    TotalFee = table.Column<int>(nullable: false),
                    TradeNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayLogEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayLogEntity_CustomerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayLogEntity_OrderEntity_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderEntity",
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
                    Number = table.Column<int>(nullable: false),
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
                name: "ShoppingBasketEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    GoodId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBasketEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingBasketEntity_CustomerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingBasketEntity_GoodEntity_GoodId",
                        column: x => x.GoodId,
                        principalTable: "GoodEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCoupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CanUse = table.Column<bool>(nullable: false),
                    CouponMappingId = table.Column<Guid>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
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
                        name: "FK_CustomerCoupon_CustomerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_UserAccountId",
                table: "ActivityLog",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponEntity_OrderEntityId",
                table: "CouponEntity",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_UserAccountId",
                table: "CustomerAccount",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCoupon_CouponMappingId",
                table: "CustomerCoupon",
                column: "CouponMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCoupon_CustomerId",
                table: "CustomerCoupon",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfo_CustomerId",
                table: "CustomerInfo",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerYanzhi_CustomerAccountId",
                table: "CustomerYanzhi",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodComments_CustomerAccountId",
                table: "GoodComments",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodEntity_CategoryId",
                table: "GoodEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodEntity_GoodOfferId",
                table: "GoodEntity",
                column: "GoodOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_CustomerAccountId",
                table: "OrderEntity",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExpressInfo_OrderEntityId",
                table: "OrderExpressInfo",
                column: "OrderEntityId",
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
                name: "IX_PayAPILog_UserAccountId",
                table: "PayAPILog",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayLogEntity_CustomerId",
                table: "PayLogEntity",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PayLogEntity_OrderId",
                table: "PayLogEntity",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBasketEntity_CustomerId",
                table: "ShoppingBasketEntity",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBasketEntity_GoodId",
                table: "ShoppingBasketEntity",
                column: "GoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentEntry_Categories_CategoryId",
                table: "ContentEntry",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntry_Categories_CategoriesId",
                table: "FileEntry",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Tags_Categories_CategoriesId",
            //    table: "Tags",
            //    column: "CategoriesId",
            //    principalTable: "Categories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
