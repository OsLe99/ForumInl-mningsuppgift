using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ForumInlämningsuppgift.Pages.AdminPages
{
    public class AdminIndexModel : PageModel
    {
        private readonly UserManager<ForumUser> _userManager;

        public AdminIndexModel(UserManager<ForumUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;

            if (currentUser == null || !currentUser.IsAdmin)
            {
                return Forbid();
            }

            // Admin stuff


            return Page();
        }
    }
}
