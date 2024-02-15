using System.ComponentModel.DataAnnotations;

namespace NoteTakingAppWithLogin.Shared
{
    public class UserNote
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Title { get; set; }  =string.Empty;


        [Required]
        [StringLength(4000)]
        public required string Content { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public required string Tag { get; set; } = string.Empty;



        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
    }
}
