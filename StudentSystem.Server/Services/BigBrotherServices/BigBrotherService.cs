using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Server.Services.BigBrotherServices
{
    public class BigBrotherService : IBigBrotherService
    {
        private readonly DataContext _dataContext;

        public BigBrotherService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<GroupChat>> GetAllGroup(int id)
        {
            var user = await _dataContext.Users
                            .Include(user => user.GroupChats)
                                .ThenInclude(user => user.Members)
                            .FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return new List<GroupChat>();
            }

            return user.GroupChats;
        }

        public async Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId, int userId)
        {
            List<GroupChatMessage> groupChatMessages = await _dataContext.GroupChatMessages
                    .Include(a => a.User)
                    .Include(message => message.GroupChat)
                        .ThenInclude(message => message.Members)
                    .Where(message => message.GroupChatId == groupChatId 
                    && message.GroupChat.Members.Any(m => m.Id == userId)
                    && message.Timestamp.Date == DateTime.Now.Date)
                    
                    .OrderBy(message => message.Timestamp)
                    .Select(x => new GroupChatMessage
                    {
                        Content = x.Content,
                        Timestamp = x.Timestamp,
                        UserId = x.UserId,
                        GroupChatId = x.GroupChatId,
                        Id = x.Id,
                        User = x.User
                    }).ToListAsync(); 

            return groupChatMessages;
        }

        public async Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId, int userId)
        {
            GroupChat? myGroupChat = await _dataContext.GroupChats
                                  .Where(gc => gc.Members.Any(m => m.Id == userId))
                                  .Include(gc => gc.Members)
                                  .FirstOrDefaultAsync(gc => gc.Id == groupChatId);

            if (myGroupChat != null && myGroupChat.Members != null)
            {
                GetChatMembersDTO response = new GetChatMembersDTO();
                response.OwnerId = myGroupChat.OwnerId;
                response.users = myGroupChat.Members.Select(user => new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    Avatar = user.Avatar,

                }).ToList();

                return response;
            }
            return new GetChatMembersDTO();
        }

        public Task<string?> GetGroupName(int groupChatId)
        {
            throw new NotImplementedException();
        }
    }
}
