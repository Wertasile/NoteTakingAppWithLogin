using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteTakingAppWithLogin.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFavouriteToUserNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Favourite",
                table: "UserNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favourite",
                table: "UserNotes");
        }
    }
}
