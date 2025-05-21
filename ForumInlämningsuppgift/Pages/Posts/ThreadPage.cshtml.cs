using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ForumInlämningsuppgift.Pages.Posts
{
    public class ThreadPageModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ForumUser> _userManager;

        public ThreadPageModel(Data.ApplicationDbContext context, UserManager<ForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Post Post { get; set; }
        public List<Reply> Replies { get; set; }
        [BindProperty]
        public Reply NewReply { get; set; }

        public async Task<IActionResult> OnGetAsync(int threadId)
        {
            Post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.SubCategory)
                .ThenInclude(sc => sc.Category)
                .FirstOrDefaultAsync(p => p.Id == threadId);
            if (Post == null)
            {
                return NotFound();
            }

            Replies = await _context.Replies
                .Where(r => r.PostId == threadId)
                .Include(r => r.User)
                .OrderBy(r => r.Date)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int threadId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            NewReply.Date = DateTime.Now;
            NewReply.UserId = currentUser.Id;
            NewReply.PostId = threadId;

            if (!ModelState.IsValid)
            {
                Post = await _context.Posts
                    .Include(p => p.User)
                    .Include(p => p.SubCategory)
                    .FirstOrDefaultAsync(p => p.Id == threadId);

                Replies = await _context.Replies
                    .Where(r => r.PostId == threadId)
                    .Include(r => r.User)
                    .OrderBy(r => r.Date)
                    .ToListAsync();

                return Page();
            }

            if (currentUser == null)
            {
                return Forbid();
            }

            _context.Replies.Add(NewReply);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { threadId });
        }
    }
}
