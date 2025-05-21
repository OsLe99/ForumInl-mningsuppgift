using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ForumInlämningsuppgift.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public string? Image { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string UserId { get; set; }
        [ValidateNever]
        public virtual ForumUser User { get; set; }

        public int SubCategoryId { get; set; }
        [ValidateNever]
        public virtual SubCategory SubCategory { get; set; }
    }
}
