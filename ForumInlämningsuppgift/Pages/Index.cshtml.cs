using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Pages;

public class IndexModel : PageModel
{
    private readonly Data.ApplicationDbContext _context;

    public IndexModel(Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Category Category { get; set; }
    public List<Models.Category> Categories { get; set; }

    public async Task OnGetAsync()
    {
        Categories = await _context.Category.Include(c=> c.SubCategories).ToListAsync();
    }
}
