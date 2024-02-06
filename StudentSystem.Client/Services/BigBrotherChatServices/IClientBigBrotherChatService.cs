using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.BigBrotherChatServices
{
    public interface IClientBigBrotherChatService
    {
        Task<List<ChatMessage>> GetConversationAsync(int receiversId, int sendersId);
    }
}
