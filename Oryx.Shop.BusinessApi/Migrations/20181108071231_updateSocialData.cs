using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateSocialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<Guid>(
            //    name: "ContentEntryId",
            //    table: "Tags",
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "PosterId",
            //    table: "PostEntryTopic",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateTable(
            //    name: "SandBoxMessage",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        AttentionInfo = table.Column<string>(nullable: true),
            //        Content = table.Column<string>(nullable: true),
            //        CreateTime = table.Column<DateTime>(nullable: false),
            //        FromUserAccountId = table.Column<Guid>(nullable: false),
            //        IsRead = table.Column<bool>(nullable: false),
            //        MessageType = table.Column<int>(nullable: false),
            //        RecieveToken = table.Column<string>(nullable: true),
            //        TimeStamp = table.Column<long>(nullable: false),
            //        ToUserAccountId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SandBoxMessage", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SandBoxMessage_UserAccountEntry_FromUserAccountId",
            //            column: x => x.FromUserAccountId,
            //            principalTable: "UserAccountEntry",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_SandBoxMessage_UserAccountEntry_ToUserAccountId",
            //            column: x => x.ToUserAccountId,
            //            principalTable: "UserAccountEntry",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserSocialRelationship",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        CreateTime = table.Column<DateTime>(nullable: false),
            //        RelationConnectorId = table.Column<Guid>(nullable: false),
            //        RelationOnwerId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserSocialRelationship", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserSocialRelationship_UserAccountEntry_RelationConnectorId",
            //            column: x => x.RelationConnectorId,
            //            principalTable: "UserAccountEntry",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserSocialRelationship_UserAccountEntry_RelationOnwerId",
            //            column: x => x.RelationOnwerId,
            //            principalTable: "UserAccountEntry",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tags_ContentEntryId",
            //    table: "Tags",
            //    column: "ContentEntryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PostEntryTopic_PosterId",
            //    table: "PostEntryTopic",
            //    column: "PosterId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SandBoxMessage_FromUserAccountId",
            //    table: "SandBoxMessage",
            //    column: "FromUserAccountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SandBoxMessage_ToUserAccountId",
            //    table: "SandBoxMessage",
            //    column: "ToUserAccountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserSocialRelationship_RelationConnectorId",
            //    table: "UserSocialRelationship",
            //    column: "RelationConnectorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserSocialRelationship_RelationOnwerId",
            //    table: "UserSocialRelationship",
            //    column: "RelationOnwerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PostEntryTopic_UserAccountEntry_PosterId",
            //    table: "PostEntryTopic",
            //    column: "PosterId",
            //    principalTable: "UserAccountEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Tags_ContentEntry_ContentEntryId",
            //    table: "Tags",
            //    column: "ContentEntryId",
            //    principalTable: "ContentEntry",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntryTopic_UserAccountEntry_PosterId",
                table: "PostEntryTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_ContentEntry_ContentEntryId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "SandBoxMessage");

            migrationBuilder.DropTable(
                name: "UserSocialRelationship");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ContentEntryId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_PostEntryTopic_PosterId",
                table: "PostEntryTopic");

            migrationBuilder.DropColumn(
                name: "ContentEntryId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PosterId",
                table: "PostEntryTopic");
        }
    }
}
