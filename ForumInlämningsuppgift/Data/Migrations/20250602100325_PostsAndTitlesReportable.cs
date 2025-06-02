using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumInlämningsuppgift.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostsAndTitlesReportable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Replies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Posts");
        }
    }
}
