using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.BigBrotherServices
{
    public interface IClientBigBrotherService
    {
        Task<List<GroupChat>> GetAllGroup(int id);
        Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId, int userId);
        Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId, int id);

    }
}
