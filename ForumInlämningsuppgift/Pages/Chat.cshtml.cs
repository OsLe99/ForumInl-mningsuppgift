using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Pages
{
    public class ChatModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ForumUser> _userManager;

        public ChatModel(Data.ApplicationDbContext context, UserManager<ForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public string MessageText { get; set; }

        public ForumUser Recipient { get; set; }
        public List<Message> Messages { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var currentUserId = _userManager.GetUserId(User);

            Recipient = await _userManager.FindByIdAsync(userId);
            if (Recipient == null || currentUserId == userId)
            {
                return NotFound();
            }

            Messages = await _context.Messages
                .Where(m => (m.SenderId == currentUserId && m.RecipientId == userId) ||
                            (m.SenderId == userId && m.RecipientId == currentUserId))
                .OrderBy(m => m.Date)
                .ToListAsync();

            var unreadMessages = Messages
                .Where(m => m.RecipientId == currentUserId && !m.IsRead)
                .ToList();

            if (unreadMessages.Any())
            {
                foreach (var msg in unreadMessages)
                {
                    msg.IsRead = true;
                }
                await _context.SaveChangesAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string userId)
        {
            var senderId = _userManager.GetUserId(User);

            if (string.IsNullOrWhiteSpace(MessageText))
            {
                return RedirectToPage(new { userId });
            }

            var message = new Message
            {
                SenderId = senderId,
                RecipientId = userId,
                Text = MessageText,
                Date = DateTime.Now,
                IsRead = false
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { userId });
        }
    }
}
