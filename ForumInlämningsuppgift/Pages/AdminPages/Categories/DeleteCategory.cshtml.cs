using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using ForumInlämningsuppgift.DAL;

namespace ForumInlämningsuppgift.Pages.AdminPages.Categories
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly CategoryManager _categoryManager;

        public DeleteCategoryModel(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [BindProperty]
        public Category CategoryToDelete { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _categoryManager.DeleteCategoryAsync(id);
            return RedirectToPage("/AdminPages/Categories/CategoryList");
        }
    }
}
