using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using ForumInlämningsuppgift.DAL;

namespace ForumInlämningsuppgift.Pages.AdminPages.Categories
{
    public class CategoryListModel : PageModel
    {
        private readonly CategoryManager _categoryManager;

        public CategoryListModel(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        public List<Category> Categories { get; set; }

        public async Task  OnGetAsync()
        {
            Categories = await _categoryManager.GetAllCategoriesAsync();
        }
    }
}
