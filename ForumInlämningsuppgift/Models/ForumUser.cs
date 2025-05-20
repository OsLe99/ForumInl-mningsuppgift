using Microsoft.AspNetCore.Identity;
namespace ForumInlämningsuppgift.Models
{
    public class ForumUser : IdentityUser
    {
        public string? ProfileImage { get; set; }
        public string? Nickname { get; set; }
        public bool IsAdmin { get; set; }
    }
}
