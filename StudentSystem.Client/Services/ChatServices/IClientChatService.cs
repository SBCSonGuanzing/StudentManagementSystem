using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.ChatServices
{
    public interface IClientChatService
    {
        Task<List<User>> GetUsersAsync();
        Task SaveMessageAsync(ChatMessage message);
        Task<List<ChatMessage>> GetConversationAsync(int contactId);
        Task<User?> GetUserDetailsAsync(int userId);
    }
}
