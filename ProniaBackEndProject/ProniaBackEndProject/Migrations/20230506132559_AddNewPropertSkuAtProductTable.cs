﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaBackEndProject.Migrations
{
    public partial class AddNewPropertSkuAtProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sku",
                table: "Products");
        }
    }
}
