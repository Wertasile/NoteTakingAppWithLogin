using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteTakingAppWithLogin.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncludingUserIdColumnforTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            {
                migrationBuilder.AddColumn<string>(
                    name: "ApplicationUserId",
                    table: "Tags",
                    nullable: true);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
