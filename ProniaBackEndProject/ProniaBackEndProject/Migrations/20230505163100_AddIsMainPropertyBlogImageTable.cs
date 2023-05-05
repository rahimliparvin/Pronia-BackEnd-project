﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaBackEndProject.Migrations
{
    public partial class AddIsMainPropertyBlogImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "BlogImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "BlogImages");
        }
    }
}
