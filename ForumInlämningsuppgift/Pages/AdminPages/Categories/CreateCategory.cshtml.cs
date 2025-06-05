using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using ForumInlämningsuppgift.DAL;

namespace ForumInlämningsuppgift.Pages.AdminPages.Categories
{
    public class CreateCategoryModel : PageModel
    {
        private readonly CategoryManager _categoryManager;

        public CreateCategoryModel(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [BindProperty]
        public Category NewCategory { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _categoryManager.CreateCategoryAsync(NewCategory);
            return RedirectToPage("/AdminPages/Categories/CategoryList");
        }
    }
}
