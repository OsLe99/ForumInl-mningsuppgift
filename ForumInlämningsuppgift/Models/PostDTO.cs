namespace ForumInlämningsuppgift.Models
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string? Image { get; set; }
        public DateTime Date { get; set; }
        public int SubCategoryId { get; set; }
        public string UserId { get; set; }
    }
}
