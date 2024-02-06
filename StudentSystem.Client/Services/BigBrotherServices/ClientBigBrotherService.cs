using Microsoft.AspNetCore.Components;
using MudBlazor;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace StudentSystem.Client.Services.BigBrotherServices
{
    public class ClientBigBrotherService : IClientBigBrotherService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;
        private readonly NavigationManager _navigationManager;
        public ClientBigBrotherService(HttpClient httpClient, ISnackbar snackbar, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
            _navigationManager = navigationManager;
        }

        public async Task<List<GroupChat>> GetAllGroup(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<GroupChat>>($"api/BigBrother/all-groups/{id}");

        }

        public async Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId, int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<GroupChatMessage>>($"api/BigBrother/get-convo/{groupChatId}/{userId}");
        }

        public async Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId, int id)
        {
            var result = await _httpClient.GetAsync($"api/BigBrother/users-from-group/{groupChatId}/{id}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var users = await result.Content.ReadFromJsonAsync<GetChatMembersDTO>();
                return users;
            }

            return new GetChatMembersDTO();
        }
    }
}
