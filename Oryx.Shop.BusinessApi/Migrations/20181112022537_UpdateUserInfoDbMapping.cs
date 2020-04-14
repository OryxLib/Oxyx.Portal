using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class UpdateUserInfoDbMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAccountEntry_UserAccountEmail_EmailId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAccountEntry_MoneyWallet_MoneyWalletId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAccountEntry_UserAccountPhone_PhoneId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAccountEntry_UserAccountProfile_ProfileId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAccountEntry_RewardPointWallet_RewardPointWalletId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAccountEntry_UserAccountUserNamePwd_UserNamePwdId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAccountEntry_UserAccountWeChat_WeChatId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserAccountEntry_EmailId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserAccountEntry_MoneyWalletId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserAccountEntry_PhoneId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserAccountEntry_ProfileId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserAccountEntry_RewardPointWalletId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserAccountEntry_UserNamePwdId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserAccountEntry_WeChatId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropColumn(
            //    name: "EmailId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropColumn(
            //    name: "MoneyWalletId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropColumn(
            //    name: "PhoneId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropColumn(
            //    name: "ProfileId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropColumn(
            //    name: "RewardPointWalletId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropColumn(
            //    name: "UserNamePwdId",
            //    table: "UserAccountEntry");

            //migrationBuilder.DropColumn(
            //    name: "WeChatId",
            //    table: "UserAccountEntry");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "UserAccountWeChat",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "UserAccountUserNamePwd",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "UserAccountProfile",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "UserAccountPhone",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "UserAccountEmail",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "RewardPointWallet",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserAccountId",
            //    table: "MoneyWallet",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAccountWeChat_UserAccountId",
            //    table: "UserAccountWeChat",
            //    column: "UserAccountId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAccountUserNamePwd_UserAccountId",
            //    table: "UserAccountUserNamePwd",
            //    column: "UserAccountId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAccountProfile_UserAccountId",
            //    table: "UserAccountProfile",
            //    column: "UserAccountId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAccountPhone_UserAccountId",
            //    table: "UserAccountPhone",
            //    column: "UserAccountId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAccountEmail_UserAccountId",
            //    table: "UserAccountEmail",
            //    column: "UserAccountId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RewardPointWallet_UserAccountId",
            //    table: "RewardPointWallet",
            //    column: "UserAccountId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_MoneyWallet_UserAccountId",
            //    table: "MoneyWallet",
            //    column: "UserAccountId",
            //    unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_MoneyWallet_UserAccountEntry_UserAccountId",
            //    table: "MoneyWallet",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_RewardPointWallet_UserAccountEntry_UserAccountId",
            //    table: "RewardPointWallet",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserAccountEmail_UserAccountEntry_UserAccountId",
            //    table: "UserAccountEmail",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserAccountPhone_UserAccountEntry_UserAccountId",
            //    table: "UserAccountPhone",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserAccountProfile_UserAccountEntry_UserAccountId",
            //    table: "UserAccountProfile",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserAccountUserNamePwd_UserAccountEntry_UserAccountId",
            //    table: "UserAccountUserNamePwd",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserAccountWeChat_UserAccountEntry_UserAccountId",
            //    table: "UserAccountWeChat",
            //    column: "UserAccountId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyWallet_UserAccountEntry_UserAccountId",
                table: "MoneyWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_RewardPointWallet_UserAccountEntry_UserAccountId",
                table: "RewardPointWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountEmail_UserAccountEntry_UserAccountId",
                table: "UserAccountEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountPhone_UserAccountEntry_UserAccountId",
                table: "UserAccountPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountProfile_UserAccountEntry_UserAccountId",
                table: "UserAccountProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountUserNamePwd_UserAccountEntry_UserAccountId",
                table: "UserAccountUserNamePwd");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccountWeChat_UserAccountEntry_UserAccountId",
                table: "UserAccountWeChat");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountWeChat_UserAccountId",
                table: "UserAccountWeChat");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountUserNamePwd_UserAccountId",
                table: "UserAccountUserNamePwd");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountProfile_UserAccountId",
                table: "UserAccountProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountPhone_UserAccountId",
                table: "UserAccountPhone");

            migrationBuilder.DropIndex(
                name: "IX_UserAccountEmail_UserAccountId",
                table: "UserAccountEmail");

            migrationBuilder.DropIndex(
                name: "IX_RewardPointWallet_UserAccountId",
                table: "RewardPointWallet");

            migrationBuilder.DropIndex(
                name: "IX_MoneyWallet_UserAccountId",
                table: "MoneyWallet");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserAccountWeChat");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserAccountUserNamePwd");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserAccountProfile");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserAccountPhone");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "UserAccountEmail");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "RewardPointWallet");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "MoneyWallet");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MoneyWalletId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhoneId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RewardPointWalletId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserNamePwdId",
                table: "UserAccountEntry",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WeChatId",
                table: "UserAccountEntry",
                nullable: true);

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
                name: "IX_UserAccountEntry_ProfileId",
                table: "UserAccountEntry",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_RewardPointWalletId",
                table: "UserAccountEntry",
                column: "RewardPointWalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_UserNamePwdId",
                table: "UserAccountEntry",
                column: "UserNamePwdId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountEntry_WeChatId",
                table: "UserAccountEntry",
                column: "WeChatId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_UserAccountEmail_EmailId",
                table: "UserAccountEntry",
                column: "EmailId",
                principalTable: "UserAccountEmail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_MoneyWallet_MoneyWalletId",
                table: "UserAccountEntry",
                column: "MoneyWalletId",
                principalTable: "MoneyWallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_UserAccountPhone_PhoneId",
                table: "UserAccountEntry",
                column: "PhoneId",
                principalTable: "UserAccountPhone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_UserAccountProfile_ProfileId",
                table: "UserAccountEntry",
                column: "ProfileId",
                principalTable: "UserAccountProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_RewardPointWallet_RewardPointWalletId",
                table: "UserAccountEntry",
                column: "RewardPointWalletId",
                principalTable: "RewardPointWallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_UserAccountUserNamePwd_UserNamePwdId",
                table: "UserAccountEntry",
                column: "UserNamePwdId",
                principalTable: "UserAccountUserNamePwd",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccountEntry_UserAccountWeChat_WeChatId",
                table: "UserAccountEntry",
                column: "WeChatId",
                principalTable: "UserAccountWeChat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
