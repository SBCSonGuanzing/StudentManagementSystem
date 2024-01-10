
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.EntityFrameworkCore;
//using StudentSystem.Server.Data;
//using StudentSystem.Shared.Models;
//using System.Security.Claims;

//namespace StudentSystem.Server.Services.ChatServices
//{
//    public class ChatService : IChatService
//    {
//        private readonly DataContext _context;
//        private readonly IHttpContextAccessor _contextAccessor;

//        public ChatService(DataContext context,IHttpContextAccessor contextAccessor)
//        {
//            _context = context;
//            _contextAccessor = contextAccessor;
//        }
//        public async Task<List<User>> GetAllUsers()
//        {
//            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//            List<User> users = await _context.Users
//              .Include(p => p.Student)
//              .Include(p => p.Professor)
//              .Where(user => user.Id.ToString() != userId)
//              .ToListAsync();

//            return users;
//        }

//        public async Task<List<ChatMessage>> GetConversationAsync(int id)
//        {
//            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            var messages = await _context.ChatMessages
//                    .Where(h => (h.FromUserId == id && h.ToUserId.ToString() == userId) || (h.FromUserId.ToString() == userId && h.ToUserId == id))
//                    .OrderBy(a => a.Timestamp)
//                    .Include(a => a.FromUser)
                  

//                    .Select(x => new ChatMessage
//                    {
//                        FromUserId = x.FromUserId,
//                        Content = x.Content,
//                        Timestamp = x.Timestamp,
//                        Id = x.Id,
//                        ToUserId = x.ToUserId,
//                        ToUser = x.ToUser,
//                        FromUser = x.FromUser
//                    }).ToListAsync();

//            return messages;
//        }

//        public async Task<User> GetSingleUser(int id)
//        {
//            var user = await _context.Users
//                     .Where(p => p.Id == id)
//                     .FirstOrDefaultAsync();
//            if (user == null)
//                return null;

//            return user;
//        }

//        public async Task SaveMessagesAsyc(ChatMessage message)
//        {
//            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
         
//            message.FromUserId = int.Parse(userId);
//            message.Timestamp = DateTime.Now;
//            //message.ToUser = await _context.Users
//            //    .Where(user => user.Id == message.ToUserId)
//            //    .FirstOrDefault();

//            await _context.ChatMessages.AddAsync(message);
//            await _context.SaveChangesAsync();
//        }


//    }
//}
