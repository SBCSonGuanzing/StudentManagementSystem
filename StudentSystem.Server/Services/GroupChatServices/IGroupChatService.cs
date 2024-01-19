using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.GroupChatServices
{
    public interface IGroupChatService
    {
        Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId);
        Task SaveMessagesAsync(GroupChatMessage message);
        Task<List<User>> GetUsersAsync();
        Task<string> GetGroupName(int groupChatId); 
        Task<List<GroupChat>> GetAllGroup();
        Task<User> GetUserDetailsAsync(int userId);
        Task<List<GroupChat>> CreateGroupChat(GroupChatDTO request);
        Task<List<GroupChat>> RemoveGroupChat(int groupChatId);
        Task<List<User>> GetGroupChatMembers(int groupChatId);
        Task<bool> AddUserToGroup(int userId, int groupChatId);
        Task<bool> RemoveUserToGroup(int userId, int groupChatId);

    }
}
