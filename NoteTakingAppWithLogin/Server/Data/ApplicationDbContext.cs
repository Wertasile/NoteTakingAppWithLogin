using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NoteTakingAppWithLogin.Server.Models;
using NoteTakingAppWithLogin.Shared;

namespace NoteTakingAppWithLogin.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<UserNote> UserNotes => Set<UserNote>();  //USER NOTES TABLE
        public DbSet<Tag> Tags => Set<Tag>();  // TAGS TABLE
    }
}







