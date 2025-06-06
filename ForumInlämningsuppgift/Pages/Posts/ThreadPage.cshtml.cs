using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Security.Claims;

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

        [BindProperty(SupportsGet = true)]
        public int ThreadId { get; set; }

        public Post Post { get; set; }
        public List<Reply> Replies { get; set; }
        [BindProperty]
        public Reply NewReply { get; set; }
        [BindProperty]
        public IFormFile? ReplyImage { get; set; }

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

            if (ReplyImage != null && ReplyImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "replies");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ReplyImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ReplyImage.CopyToAsync(fileStream);
                }
                NewReply.Image = "/images/replies/" + uniqueFileName;
            }

            _context.Replies.Add(NewReply);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { threadId });

        }
        // Report post
        public async Task<IActionResult> OnPostReportPostAsync(int threadId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Forbid();
            }

            var post = await _context.Posts.FindAsync(threadId);
            if (post == null)
            {
                return NotFound();
            }
            post.IsReported = true;
            await _context.SaveChangesAsync();
            TempData["ReportMessage"] = "Post has been reported successfully.";
            return RedirectToPage(new { threadId });
        }
        // Report reply
        public async Task<IActionResult> OnPostReportReplyAsync(int replyId, int threadId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Forbid();
            }

            var reply = await _context.Replies.FindAsync(replyId);
            if (reply == null)
            {
                return NotFound();
            }
            reply.IsReported = true;
            await _context.SaveChangesAsync();
            TempData["ReportMessage"] = "Reply has been reported successfully.";
            return RedirectToPage(new { threadId });
        }
    }
}
