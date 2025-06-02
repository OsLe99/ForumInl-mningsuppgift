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
        private readonly UserManager<ForumUser> _userManager;

        public CreateNewThreadModel(Data.ApplicationDbContext context, UserManager<ForumUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Post Post { get; set; }
        public SelectList SubCategoryList { get; set; }
        [BindProperty]
        public IFormFile? PostImage { get; set; }

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
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }
            //Post.UserId = currentUser.Id;

            if (!ModelState.IsValid)
            {
                SubCategoryList = new SelectList(await _context.SubCategory.ToListAsync(), "Id", "Name");
                return Page();
            }

            if (PostImage != null && PostImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "posts");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(PostImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PostImage.CopyToAsync(fileStream);
                }
                Post.Image = "/images/posts/" + uniqueFileName;
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/SubCategories/Details", new { id = Post.SubCategoryId });
        }
    }
}
