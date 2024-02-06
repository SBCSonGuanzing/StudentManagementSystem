using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.BigBrotherServices
{
    public interface IBigBrotherService
    {
        Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId, int userId);
        Task<string?> GetGroupName(int groupChatId);
        Task<List<GroupChat>> GetAllGroup(int id);
        Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId, int userId);

    }
}
