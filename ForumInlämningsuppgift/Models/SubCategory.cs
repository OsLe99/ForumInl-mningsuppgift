using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ForumInlämningsuppgift.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        [BindNever]
        [ValidateNever]
        public virtual Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [BindNever]
        [ValidateNever]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
