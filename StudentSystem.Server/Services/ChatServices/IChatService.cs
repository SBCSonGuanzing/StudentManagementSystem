using Microsoft.AspNetCore.Mvc;

namespace StudentSystem.Server.Services.ChatServices
{
    public interface IChatService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserDetailsAsync(int userId);
        Task SaveMessagesAsyc(ChatMessage message);
        Task<List<ChatMessage>> GetConversationAsync(int receiverId);
    }
}
