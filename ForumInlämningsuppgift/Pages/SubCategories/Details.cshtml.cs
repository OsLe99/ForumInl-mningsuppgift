using ForumInlämningsuppgift.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Pages.SubCategory
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        public DetailsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.SubCategory SubCategory { get; set; }

        public List<Models.Post> Posts { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            SubCategory = await _context.SubCategory.Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (SubCategory == null)
            {
                return RedirectToPage("/Error");
            }

            Posts = await _context.Posts
                .Include(p => p.User)
                .Where(p => p.SubCategoryId == id)
                .ToListAsync();

            return Page();
        }
    }
}
