using ForumInlämningsuppgift.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForumInlämningsuppgift.Pages.AdminPages.SubCategories
{
    public class SubCategoryListModel : PageModel
    {
        private readonly CategoryManager _categoryManager;

        public SubCategoryListModel(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public List<Models.SubCategory> SubCategories { get; set; }

        public async Task OnGetAsync()
        {
            SubCategories = await _categoryManager.GetAllSubCategoriesAsync();
        }
    }
}
