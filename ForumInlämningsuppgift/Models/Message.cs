using System.ComponentModel.DataAnnotations;

namespace ForumInlämningsuppgift.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }
        public ForumUser Sender { get; set; }

        [Required]
        public string RecipientId { get; set; }
        public ForumUser Recipient { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
