using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Oryx.Shop.BusinessApi.Migrations
{
    public partial class updateCustomeInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CustomerInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "CustomerInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "CustomerInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "CustomerInfo");

            migrationBuilder.DropColumn(
                name: "District",
                table: "CustomerInfo");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "CustomerInfo");
        }
    }
}
