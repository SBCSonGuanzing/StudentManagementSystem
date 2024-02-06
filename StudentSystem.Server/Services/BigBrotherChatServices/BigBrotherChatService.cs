
using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Data;

namespace StudentSystem.Server.Services.BigBrotherChatServices
{
    public class BigBrotherChatService : IBigBrotherChatService
    {
        private readonly DataContext _context;

        public BigBrotherChatService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ChatMessage>> GetConversationAsync(int receiverId, int senderId)
        {
            List<ChatMessage> messages = await _context.ChatMessages
                   .Where(h => (h.FromUserId == receiverId && h.ToUserId.ToString() == senderId.ToString())
                       || (h.FromUserId.ToString() == senderId.ToString() && h.ToUserId == receiverId))
                   .OrderBy(a => a.Timestamp)
                   .Include(a => a.User)
                   .Select(x => new ChatMessage
                   {
                       FromUserId = x.FromUserId,
                       ToUserId = x.ToUserId,
                       Content = x.Content,
                       Timestamp = x.Timestamp,
                       Id = x.Id,
                       User = x.User
                   }).ToListAsync();

            return messages;
        }
    }
}
