namespace StudentSystem.Server.Services.BigBrotherChatServices
{
    public interface IBigBrotherChatService
    {
        Task<List<ChatMessage>> GetConversationAsync(int receiverId, int senderId);

    }
}
