using ForumInlämningsuppgift.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Data;

public class ApplicationDbContext : IdentityDbContext<ForumUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Models.Category> Category { get; set; }
    public DbSet<Models.SubCategory> SubCategory { get; set; }
    public DbSet<Models.Post> Posts { get; set; }
}
