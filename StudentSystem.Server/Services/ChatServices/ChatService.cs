using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Data;
using StudentSystem.Shared.Models;
using System.Security.Claims;

namespace StudentSystem.Server.Services.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ChatService(DataContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<User> users = await _context.Users
              .Where(user => user.Id.ToString() != userId)
              .ToListAsync();

            return users;
        }

        public async Task<List<ChatMessage>> GetConversationAsync(int receiverId)
        {
            var senderId = _contextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

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

        public async Task<User> GetUserDetailsAsync(int userId)
        {
            User? user = await _context.Users
                     .Where(p => p.Id == userId)
                     .FirstOrDefaultAsync();
            if (user == null)
                return null;
             
            return user;
        }

        // TODO: Create a DTO
        public async Task SaveMessagesAsyc(ChatMessage message)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return;

            message.FromUserId = int.Parse(userId);
            message.Timestamp = DateTime.Now;
            message.ToUserId = message.ToUserId;
            message.Content = message.Content;
            message.User = await _context.Users.FindAsync(message.FromUserId);

            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();

            
        }
    }
}
