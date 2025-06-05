using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForumInlämningsuppgift.Pages.AdminPages.SubCategories
{
    public class DeleteSubCategoryModel : PageModel
    {
        private readonly DAL.CategoryManager _categoryManager;

        public DeleteSubCategoryModel(DAL.CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [BindProperty]
        public Models.SubCategory SubCategoryToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SubCategoryToDelete = await _categoryManager.GetSubCategoryByIdAsync(id);
            if (SubCategoryToDelete == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SubCategoryToDelete == null)
            {
                return NotFound();
            }
            await _categoryManager.DeleteSubCategoryAsync(SubCategoryToDelete.Id);
            return RedirectToPage("/AdminPages/SubCategories/SubCategoryList");
        }
    }
}
