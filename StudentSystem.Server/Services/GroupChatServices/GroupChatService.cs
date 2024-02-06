using Azure.Core;
using Microsoft.AspNetCore.Components;
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
        private readonly NavigationManager _navigationManager;

        public GroupChatService(DataContext dataContext, IHttpContextAccessor contextAccessor, NavigationManager navigationManager)
        {
            _dataContext = dataContext;
            _contextAccessor = contextAccessor;
            _navigationManager = navigationManager;
        }

        public async Task<GroupChat?> AddUserToGroup(int userId, int groupChatId)
        {
            try
            {
                var groupExisting = await _dataContext.GroupChats
                    .Where(group => group.Id == groupChatId)
                    .Include(group => group.Members)
                    .FirstOrDefaultAsync();

                if (groupExisting == null)
                {
                    return null; 
                }

                if (groupExisting.Members.Any(member => member.Id == userId))
                {
                    return null; 
                }

                User? user = await _dataContext.Users.FindAsync(userId);
                if (user != null)
                {
                    groupExisting.Members.Add(user);
                    await _dataContext.SaveChangesAsync();
                    return groupExisting;
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public async Task<int> CreateGroupChat(GroupChatDTO request)
        {
            var groupExisting = await _dataContext.GroupChats
                .Where(group => group.Name == request.Name)
                .FirstOrDefaultAsync();

            if (groupExisting != null)
            {
                return 0;
            }

            var UserId = _contextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if(UserId == null)
            {
                return 0;
            }
            var newGroup = new GroupChat()
            {             
                Name = request.Name,
                OwnerId = int.Parse(UserId),
                Members = new List<User>()
            };

            foreach (int id in request.MembersId)
            {
                User member = _dataContext.Users.First(p => p.Id == id);
                newGroup.Members.Add(member);
            }

            _dataContext.GroupChats.Add(newGroup);
            await _dataContext.SaveChangesAsync();

            var user = await _dataContext.Users
                .Include(u => u.GroupChats)
                .FirstOrDefaultAsync(u => u.Id.ToString() == UserId);

            if (user != null)
            {
                user.GroupChats.Add(newGroup);
                await _dataContext.SaveChangesAsync();
            }

            return await _dataContext.GroupChats
                .Where(id => id.Name == request.Name)
                .Select(m => m.Id)
                .FirstOrDefaultAsync();
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
                var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                List<GroupChatMessage> groupChatMessages = await _dataContext.GroupChatMessages
                    .Include(a => a.User)
                    .Include(message => message.GroupChat)
                        .ThenInclude(message => message.Members)
                    .Where(message => message.GroupChatId == groupChatId && message.GroupChat.Members.Any(m => m.Id == int.Parse(userId)))
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

        public async Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId)
        {

            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            GroupChat? myGroupChat = await _dataContext.GroupChats
                                    .Where(gc => gc.Members.Any(m => m.Id == int.Parse(userId)))
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

        public async Task<string?> GetGroupName(int groupChatId)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _dataContext.GroupChats
                        .Where(group => group.Id == groupChatId && group.Members.Any(m => m.Id == int.Parse(userId)))
                        .Select(group => group.Name)
                        .FirstOrDefaultAsync();
            if(result == null)
            {
                return null;
            } 

            return result;
        }

        public async Task<List<User>> GetNotMembers(int groupId)
        {
              var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groupMembers = await GetGroupChatMembers(groupId);

            var allUsers = await _dataContext.Users
                
                .ToListAsync();

            var usersNotInGroup = allUsers
                .Where(user => !groupMembers.users.Any(groupUser => groupUser.Id == user.Id))
                .ToList();

            return usersNotInGroup;
        }

        public async Task<User?> GetUserDetailsAsync(int userId)
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

        public async Task<List<GroupChatMessage>> SaveMessagesAsync(GroupChatMessage message)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if(userId == null)
            {
                return null;
            }

            var userIsInGroup = await _dataContext.GroupChats
                            .Where(g => g.Id == message.GroupChatId)
                            .Include(g => g.Members)
                            .Select(g => g.Members)
                            .FirstOrDefaultAsync();
          
            if(userIsInGroup == null)
            {
                return null;
            }

            if(userIsInGroup.Any(m => m.Id == int.Parse(userId)))
            {
                message.Timestamp = DateTime.Now;
                message.Content = message.Content;
                message.User = await _dataContext.Users.FindAsync(int.Parse(userId));
                message.GroupChatId = message.GroupChatId;
                await _dataContext.GroupChatMessages.AddAsync(message);
                await _dataContext.SaveChangesAsync();

                
            } else
            {
                return null; 
            }

            var GroupChats = await _dataContext.GroupChatMessages
                                    .Where(m => m.GroupChatId == message.GroupChatId)
                                    .ToListAsync();

            return GroupChats;
        }

        public async Task<GroupChat?> UpdateGroupName(int groupId, GroupToUpdate groupName)
        {
            var groupToRename = await _dataContext.GroupChats.FindAsync(groupId);
            if (groupToRename != null)
            {
                groupToRename.Name = groupName.Name;
                _dataContext.SaveChanges();
            }else
            {
                return null;
            }

            return await _dataContext.GroupChats
                        .Where(m => m.Name == groupName.Name)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool?> UpdateOnlineStatus(int userId, bool onlineStatus)
        {
            var userToChangeStatus = await _dataContext.Users.FindAsync(userId);
            if(userToChangeStatus != null)
            {
                userToChangeStatus.ActiveStatus = onlineStatus;
                _dataContext.SaveChanges();
            } else
            {
                return null;
            }

            var user = await _dataContext.Users.FindAsync(userId);

            return user.ActiveStatus; 

        }
    }
} 