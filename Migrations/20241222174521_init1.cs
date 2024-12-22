using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchiveApp.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "SubCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "SubCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
