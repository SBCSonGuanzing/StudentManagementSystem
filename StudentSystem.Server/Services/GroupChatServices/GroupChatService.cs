using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Security.Claims;

namespace StudentSystem.Server.Services.GroupChatServices
{
    public class GroupChatService : IGroupChatService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public GroupChatService(DataContext dataContext, IHttpContextAccessor contextAccessor)
        {
            _dataContext = dataContext;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> AddUserToGroup(int userId, int groupChatId)
        {
            try
            {
                var user = await _dataContext.Users.FindAsync(userId);
                var groupChat = await _dataContext.GroupChats.FindAsync(groupChatId);

                if (user == null || groupChat == null)
                {
                    return false;
                }

                if (groupChat.Members.Any(m => m.Id == userId))
                {
                    return false;
                }

                groupChat.Members.Add(user);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<GroupChat>> CreateGroupChat(GroupChatDTO request)
        {
            // Check if a group chat with the same name already exists
            var groupExisting = await _dataContext.GroupChats
                .Where(group => group.Name == request.Name)
                .FirstOrDefaultAsync();

            if (groupExisting != null)
            {
                // Group chat with the same name already exists, handle accordingly
                // You may want to return an error or take appropriate action
                return null;
            }

            // Create a new group chat
            var newGroup = new GroupChat()
            {
                Name = request.Name,
            };

            // Add the new group chat to the database
            _dataContext.GroupChats.Add(newGroup);
            await _dataContext.SaveChangesAsync();

            // Get the current user's ID
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Retrieve the user from the database
            var user = await _dataContext.Users
                .Include(u => u.GroupChats)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user != null)
            {
                // Add the user to the newly created group chat
                user.GroupChats.Add(newGroup);
                await _dataContext.SaveChangesAsync();
            }

            // Return the updated list of group chats
            return await _dataContext.GroupChats.ToListAsync();
        }


        public async Task<List<GroupChat>> GetAllGroup()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _dataContext.Users
                        .Include(user => user.GroupChats)
                            .ThenInclude(user => user.Members)
                        .FirstOrDefaultAsync(user => user.Id.ToString() == userId); 

            if (user == null)
            {
                return new List<GroupChat>();
            }

            return user.GroupChats; 
        }

        public async Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId)
        {         
            List<GroupChatMessage> groupChatMessages = await _dataContext.GroupChatMessages
                .Where(message => message.GroupChatId == groupChatId)
                .OrderBy(message => message.Timestamp)
                .Include(a => a.User)
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

        public async Task<List<User>> GetGroupChatMembers(int groupChatId)
        {
            GroupChat myGroupChat = await _dataContext.GroupChats
                                    .Include(gc => gc.Members)
                                    .FirstOrDefaultAsync(gc => gc.Id == groupChatId);

            if (myGroupChat != null && myGroupChat.Members != null)
            {
                return myGroupChat.Members;
            }
            return new List<User>();
        }

        public async Task<string> GetGroupName(int groupChatId)
        {
            var result = await _dataContext.GroupChats
                        .Where(group => group.Id == groupChatId)
                        .Select(group => group.Name)
                        .FirstOrDefaultAsync();

            return result;
        }

        public async Task<User> GetUserDetailsAsync(int userId)
        {
            User? user = await _dataContext.Users
                     .Where(p => p.Id == userId)
                     .FirstOrDefaultAsync();
            if (user == null)
                return null;

            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<User> users = await _dataContext.Users
              .Where(user => user.Id.ToString() != userId)
              .ToListAsync();
            return users;
        }

        public async Task<List<GroupChat>> RemoveGroupChat(int groupChatId)
        {
            var groupChat = await _dataContext.GroupChats
                        .Where(m => m.Id == groupChatId)
                        .FirstOrDefaultAsync();

            if(groupChat == null)
            {
                return null;
            }
 
            _dataContext.Remove(groupChat);
            await _dataContext.SaveChangesAsync();

            return await _dataContext.GroupChats.ToListAsync();

        }

        public async Task<bool> RemoveUserToGroup(int userId, int groupChatId)
        {
            try
            {
                var user = await _dataContext.Users.FindAsync(userId);
                var groupChat = await _dataContext.GroupChats
                    .Include(g => g.Members)
                    .FirstOrDefaultAsync(g => g.Id == groupChatId);

                if (user == null || groupChat == null)
                {
                    return false;
                }

                var memberToRemove = groupChat.Members.FirstOrDefault(m => m.Id == userId);

                if (memberToRemove == null)
                {
                    return false;
                }

                groupChat.Members.Remove(memberToRemove);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task SaveMessagesAsync(GroupChatMessage message)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return;

            message.Timestamp = DateTime.Now;
            message.Content = message.Content;
            message.User = await _dataContext.Users.FindAsync(int.Parse(userId));
            message.GroupChatId = message.GroupChatId;
            await _dataContext.GroupChatMessages.AddAsync(message);
            await _dataContext.SaveChangesAsync();
        }
    }
}
