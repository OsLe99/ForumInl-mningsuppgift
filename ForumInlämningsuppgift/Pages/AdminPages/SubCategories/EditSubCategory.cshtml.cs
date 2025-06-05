using ForumInlämningsuppgift.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumInlämningsuppgift.Pages.AdminPages.SubCategories
{
    public class EditSubCategoryModel : PageModel
    {
        private readonly CategoryManager _categoryManager;

        public EditSubCategoryModel(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [BindProperty]
        public Models.SubCategory SubCategory { get; set; }

        public SelectList CategoryOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SubCategory = await _categoryManager.GetSubCategoryByIdAsync(id);
            if (SubCategory == null)
                return NotFound();

            var categories = await _categoryManager.GetAllCategoriesAsync();
            CategoryOptions = new SelectList(categories, "Id", "Name", SubCategory.CategoryId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryManager.GetAllCategoriesAsync();
                CategoryOptions = new SelectList(categories, "Id", "Name", SubCategory.CategoryId);
                return Page();
            }

            await _categoryManager.UpdateSubCategoryAsync(SubCategory);
            return RedirectToPage("/AdminPages/SubCategories/SubCategoryList");
        }
    }
}
