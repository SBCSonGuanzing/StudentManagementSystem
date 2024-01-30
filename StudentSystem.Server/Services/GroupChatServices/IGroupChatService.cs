using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.GroupChatServices
{
    public interface IGroupChatService
    {
        Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId);
        Task<List<GroupChatMessage>?> SaveMessagesAsync(GroupChatMessage message);
        Task<List<User>> GetUsersAsync();
        Task<string> GetGroupName(int groupChatId); 
        Task<List<GroupChat>> GetAllGroup();
        Task<User> GetUserDetailsAsync(int userId);
        Task<List<GroupChat>> CreateGroupChat(GroupChatDTO request);
        Task<List<GroupChat>> RemoveGroupChat(int groupChatId);
        Task <GetChatMembersDTO> GetGroupChatMembers(int groupChatId);
        Task<GroupChat> AddUserToGroup(int userId, int groupChatId);
        Task<bool> RemoveUserToGroup(int userId, int groupChatId);
        Task<List<User>> GetNotMembers(int groupId);
        Task<GroupChat?> UpdateGroupName(int id, GroupToUpdate groupName);
    }
}
