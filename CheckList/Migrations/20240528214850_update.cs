using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckList.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompliant",
                table: "ChecklistItems");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChecklistItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChecklistItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompliant",
                table: "ChecklistItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
