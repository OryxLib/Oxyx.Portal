using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class udpatecontentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentPayLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CostMoney = table.Column<decimal>(nullable: false),
                    CostRewardPoints = table.Column<decimal>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    PayType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPayLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyWallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Money = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyWallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RewardPointWallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    RewardPoints = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardPointWallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountEmail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountEmail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountPhone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountPhone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountWeChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OpenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountWeChat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    EmailId = table.Column<Guid>(nullable: true),
                    MoneyWalletId = table.Column<Guid>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    PhoneId = table.Column<Guid>(nullable: true),
                    RewardPointWalletId = table.Column<Guid>(nullable: true),
                    WeChatId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccountEntry_UserAccountEmail_EmailId",
                        column: x => x.EmailId,
                        principalTable: "UserAccountEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccountEntry_MoneyWallet_MoneyWalletId",
                        column: x => x.MoneyWalletId,
                        principalTable: "MoneyWallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccountEntry_UserAccountPhone_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "UserAccountPhone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccountEntry_RewardPointWallet_RewardPointWalletId",
                        column: x => x.RewardPointWalletId,
                        principalTable: "RewardPointWallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccountEntry_UserAccountWeChat_WeChatId",
                        column: x => x.WeChatId,
                        principalTable: "UserAccountWeChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ParentCommentId = table.Column<Guid>(nullable: true),
                    UserAccountId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_UserAccountEntry_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccountEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    CommentsId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentEntry_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentEntry_Comment_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActualPath = table.Column<string>(nullable: true),
                    CategoriesId = table.Column<Guid>(nullable: true),
                    ContentEntryId = table.Column<Guid>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileEntry_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileEntry_ContentEntry_ContentEntryId",
                        column: x => x.ContentEntryId,
                        principalTable: "ContentEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserAccountId",
                table: "Comment",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntry_CategoryId",
                table: "ContentEntry",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentEntry_CommentsId",
                table: "ContentEntry",
                column: "CommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntry_CategoriesId",
                table: "FileEntry",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_FileEntry_ContentEntryId",
                table: "FileEntry",
                column: "ContentEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_EmailId",
                table: "UserAccountEntry",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_MoneyWalletId",
                table: "UserAccountEntry",
                column: "MoneyWalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_PhoneId",
                table: "UserAccountEntry",
                column: "PhoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_RewardPointWalletId",
                table: "UserAccountEntry",
                column: "RewardPointWalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_WeChatId",
                table: "UserAccountEntry",
                column: "WeChatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentPayLog");

            migrationBuilder.DropTable(
                name: "FileEntry");

            migrationBuilder.DropTable(
                name: "ContentEntry");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "UserAccountEntry");

            migrationBuilder.DropTable(
                name: "UserAccountEmail");

            migrationBuilder.DropTable(
                name: "MoneyWallet");

            migrationBuilder.DropTable(
                name: "UserAccountPhone");

            migrationBuilder.DropTable(
                name: "RewardPointWallet");

            migrationBuilder.DropTable(
                name: "UserAccountWeChat");
        }
    }
}
