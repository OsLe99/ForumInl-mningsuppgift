namespace ForumInlämningsuppgift.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
