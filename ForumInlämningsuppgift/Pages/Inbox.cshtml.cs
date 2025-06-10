using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Pages
{
    public class InboxModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ForumUser> _userManager;

        public InboxModel(Data.ApplicationDbContext context,  UserManager<ForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<ForumUser> Conversations { get; set; }
        public async Task OnGetAsync()
        {
            var currentUserId = _userManager.GetUserId(User);

            Conversations = await _context.Messages
                .Where(m => m.SenderId == currentUserId || m.RecipientId == currentUserId)
                .Select(m => m.SenderId == currentUserId ? m.Recipient : m.Sender)
                .Distinct().ToListAsync();
        }
    }
}
