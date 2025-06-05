using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using ForumInlämningsuppgift.DAL;

namespace ForumInlämningsuppgift.Pages.AdminPages.Categories
{
    public class EditCategoryModel : PageModel
    {
        private readonly CategoryManager _categoryManager;

        public EditCategoryModel(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        [BindProperty]
        public Category EditedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EditedCategory = await _categoryManager.GetIdByAsync(id);
            if (EditedCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _categoryManager.UpdateCategoryAsync(EditedCategory);
            return RedirectToPage("/AdminPages/Categories/CategoryList");
        }
    }
}
