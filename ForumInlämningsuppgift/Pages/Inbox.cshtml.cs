using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;
using ForumInlämningsuppgift.Data;

namespace ForumInlämningsuppgift.Pages
{
    public class InboxModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ForumUser> _userManager;

        public InboxModel(ApplicationDbContext context, UserManager<ForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<ForumUser> Conversations { get; set; } = new();
        public Dictionary<string, int> UnreadMessageCounts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUserId = _userManager.GetUserId(User);

            var userIds = await _context.Messages
                .Where(m => m.SenderId == currentUserId || m.RecipientId == currentUserId)
                .Select(m => m.SenderId == currentUserId ? m.RecipientId : m.SenderId)
                .Distinct()
                .ToListAsync();

            Conversations = await _userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();

            UnreadMessageCounts = await _context.Messages
                .Where(m => m.RecipientId == currentUserId && !m.IsRead)
                .GroupBy(m => m.SenderId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            return Page();
        }
    }
}
