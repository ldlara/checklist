using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckList.Migrations
{
    /// <inheritdoc />
    public partial class AddIsStartedToChecklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "Checklists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "Checklists");
        }
    }
}
