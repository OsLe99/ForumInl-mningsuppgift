using ForumInlämningsuppgift.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForumInlämningsuppgift.Pages.Posts
{
    public class CreateNewThreadModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreateNewThreadModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; }
        public SelectList SubCategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? subCategoryId)
        {
            SubCategoryList = new SelectList(await _context.SubCategory.ToListAsync(), "Id", "Name");

            Post = new Post();
            if (subCategoryId != null)
            {
                Post.SubCategoryId = subCategoryId.Value;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Post.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/SubCategories/Details", new { id = Post.SubCategoryId });
        }
    }
}
