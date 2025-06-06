using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Pages.AdminPages
{
    public class ReportedPostsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public ReportedPostsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Post> ReportedPosts { get; set; } = new();
        public async Task OnGetAsync()
        {
            ReportedPosts = await _context.Posts
                .Where(p => p.IsReported)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Replies)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _context.Replies.RemoveRange(post.Replies);

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDismissAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null && post.IsReported)
            {
                post.IsReported = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
