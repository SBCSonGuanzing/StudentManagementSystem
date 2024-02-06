using StudentSystem.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;


namespace StudentSystem.Client.Services.BigBrotherChatServices
{
    public class ClientBigBrotherChatService : IClientBigBrotherChatService
    {
        private readonly HttpClient _httpClient;

        public ClientBigBrotherChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ChatMessage>> GetConversationAsync(int receiversId, int sendersId)
        {
            return await _httpClient.GetFromJsonAsync<List<ChatMessage>>($"api/BigBrotherChat/get-chat/{receiversId}/{sendersId}");           
        }
    }
}
