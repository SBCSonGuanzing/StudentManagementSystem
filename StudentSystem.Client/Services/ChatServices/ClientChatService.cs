using StudentSystem.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace StudentSystem.Client.Services.ChatServices
{
    public class ClientChatService : IClientChatService
    {
        private readonly HttpClient _httpClient;

        public ClientChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ChatMessage>> GetConversationAsync(int contactId)
        {
            return await _httpClient.GetFromJsonAsync<List<ChatMessage>>($"api/Chat/get-chat/{contactId}");
        }
        public async Task<User?> GetUserDetailsAsync(int userId)
        {
            var result = await _httpClient.GetAsync($"api/Chat/user-detail/{userId}"); 
            if(result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<User>();
            }
            return null;

        }
        public async Task<List<User>> GetUsersAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<User>>("api/Chat/users");
            return data;
        }
        public async Task SaveMessageAsync(ChatMessage message)
        {
            await _httpClient.PostAsJsonAsync("api/Chat", message);
        }
    }
}
