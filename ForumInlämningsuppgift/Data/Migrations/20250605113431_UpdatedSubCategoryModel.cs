﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumInlämningsuppgift.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSubCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Replies");
        }
    }
}
