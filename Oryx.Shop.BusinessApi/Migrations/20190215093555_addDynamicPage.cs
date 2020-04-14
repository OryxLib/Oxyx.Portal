using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class addDynamicPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ParentRouteId = table.Column<Guid>(nullable: true),
                    ParentValue = table.Column<string>(nullable: true),
                    RouteLevel = table.Column<string>(nullable: true),
                    RouteValue = table.Column<string>(nullable: true),
                    TotalRouteValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteEntry_RouteEntry_ParentRouteId",
                        column: x => x.ParentRouteId,
                        principalTable: "RouteEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReponseEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsMaster = table.Column<bool>(nullable: false),
                    MasterId = table.Column<Guid>(nullable: false),
                    PageBody = table.Column<string>(nullable: true),
                    RouteId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReponseEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReponseEntry_ReponseEntry_MasterId",
                        column: x => x.MasterId,
                        principalTable: "ReponseEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReponseEntry_RouteEntry_RouteId",
                        column: x => x.RouteId,
                        principalTable: "RouteEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReponseEntry_MasterId",
                table: "ReponseEntry",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReponseEntry_RouteId",
                table: "ReponseEntry",
                column: "RouteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteEntry_ParentRouteId",
                table: "RouteEntry",
                column: "ParentRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReponseEntry");

            migrationBuilder.DropTable(
                name: "RouteEntry");
        }
    }
}
