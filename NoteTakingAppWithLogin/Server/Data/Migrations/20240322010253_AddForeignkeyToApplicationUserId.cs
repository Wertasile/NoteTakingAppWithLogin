using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteTakingAppWithLogin.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignkeyToApplicationUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

        ALTER TABLE Tags
        ALTER COLUMN ApplicationUserId NVARCHAR(450);

        ALTER TABLE Tags
        ADD CONSTRAINT FK_Tags_AspNetUsers_ApplicationUserId FOREIGN KEY (ApplicationUserId) 
        REFERENCES AspNetUsers(Id) ON DELETE CASCADE;");
        }

        
    }
}
