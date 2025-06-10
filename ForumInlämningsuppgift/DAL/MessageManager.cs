using ForumInlämningsuppgift.Models;
using Microsoft.EntityFrameworkCore;
namespace ForumInlämningsuppgift.DAL

{
    public class MessageManager
    {
        private readonly Data.ApplicationDbContext _context;

        public MessageManager(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageAsync(string senderId, string recipientId, string text)
        {
            var message = new Message
            {
                SenderId = senderId,
                RecipientId = recipientId,
                Text = text
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetInboxAsync(string userId)
        {
            return await _context.Messages
                .Where(m => m.RecipientId == userId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<List<Message>> GetSentAsync(string userId)
        {
            return await _context.Messages
                .Where(m => m.SenderId == userId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
    }
}
