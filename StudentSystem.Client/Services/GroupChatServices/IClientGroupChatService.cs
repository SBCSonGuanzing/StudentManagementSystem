using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.GroupChatServices
{
    public interface IClientGroupChatService
    {
        Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId);
        Task SaveMessagesAsync(GroupChatMessage message);
        Task<List<User>> GetUsersAsync();
        Task<List<GroupChat>> GetAllGroup();
        Task<User> GetUserDetailsAsync(int userId);
        Task<List<GroupChat>> CreateGroupChat(GroupChatDTO request);
        Task<List<GroupChat>> RemoveGroupChat(int groupChatId);
        Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId);
        Task<GroupChat> AddUserToGroup(AddUserToGroupDTO request);
        Task<bool> RemoveUserToGroup(int userId, int groupChatId);
        Task<string> GetGroupName(int groupChatId);
        Task<List<User>> GetNotMembers(int groupId);
        Task<GroupChat> UpdateGroup(int groupId, GroupToUpdate groupName);
    }
}
