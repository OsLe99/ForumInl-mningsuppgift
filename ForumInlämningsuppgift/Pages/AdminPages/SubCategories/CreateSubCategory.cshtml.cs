using ForumInlämningsuppgift.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumInlämningsuppgift.Pages.AdminPages.SubCategories
{
    public class CreateSubCategoryModel : PageModel
    {
        private readonly CategoryManager _manager;

        public CreateSubCategoryModel(CategoryManager manager)
        {
            _manager = manager;
        }

        [BindProperty]
        public Models.SubCategory NewSubCategory { get; set; }

        public SelectList CategoryOptions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _manager.GetAllCategoriesAsync();
            CategoryOptions = new SelectList(categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _manager.GetAllCategoriesAsync();
                CategoryOptions = new SelectList(categories, "Id", "Name");
                return Page();
            }

            await _manager.CreateSubCategoryAsync(NewSubCategory);
            return RedirectToPage("SubCategoryList");
        }
    }
}
