using ForumInlämningsuppgift.Data;
using ForumInlämningsuppgift.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Pages.AdminPages
{
    public class ReportedRepliesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReportedRepliesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Reply> ReportedReplies { get; set; }

        public async Task OnGetAsync()
        {
            ReportedReplies = await _context.Replies
                .Where(r => r.IsReported)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var reply = await _context.Replies.FindAsync(id);

            if (reply != null)
            {
                _context.Replies.Remove(reply);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDismissAsync(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply != null && reply.IsReported)
            {
                reply.IsReported = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
