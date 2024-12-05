using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTL.Warehouse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Book");
        }
    }
}
