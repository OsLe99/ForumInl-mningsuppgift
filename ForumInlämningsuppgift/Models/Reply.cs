using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;

namespace ForumInlämningsuppgift.Models
{
    public class Reply
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        [ValidateNever]
        public virtual ForumUser User { get; set; }

        public int PostId { get; set; }
        [ValidateNever]
        public virtual Post Post { get; set; }
    }
}
