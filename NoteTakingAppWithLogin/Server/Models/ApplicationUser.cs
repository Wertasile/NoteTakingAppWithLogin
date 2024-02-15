using Microsoft.AspNetCore.Identity;
using NoteTakingAppWithLogin.Shared;

namespace NoteTakingAppWithLogin.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserNote> UserNotes { get; set; } = new List<UserNote>();
    }
}
