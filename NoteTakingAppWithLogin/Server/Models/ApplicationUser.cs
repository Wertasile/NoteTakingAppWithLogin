using Microsoft.AspNetCore.Identity;
using NoteTakingAppWithLogin.Shared;

namespace NoteTakingAppWithLogin.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserNote> UserNotes { get; set; } = new List<UserNote>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
