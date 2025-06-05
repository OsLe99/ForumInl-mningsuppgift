using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.DAL
{
    public class CategoryManager
    {
        private readonly Data.ApplicationDbContext _context;

        public CategoryManager(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // Category methods
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Category
                .Include(c => c.SubCategories)
                .ToListAsync();
        }

        public async Task<Category> GetIdByAsync(int id)
        {
            return await _context.Category.
                Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task CreateCategoryAsync(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Category.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        // SubCategory methods
        public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
        {
            return await _context.SubCategory.Include(s => s.Category).ToListAsync();
        }

        public async Task<SubCategory> GetSubCategoryByIdAsync(int id)
        {
            return await _context.SubCategory.Include(s => s.Category)
                                             .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategory.Add(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategory.Update(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubCategoryAsync(int id)
        {
            var subCategory = await _context.SubCategory.FindAsync(id);
            if (subCategory != null)
            {
                _context.SubCategory.Remove(subCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
