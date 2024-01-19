using Microsoft.AspNetCore.Components;
using MudBlazor;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace StudentSystem.Client.Services.GroupChatServices
{
    public class ClientGroupChatService : IClientGroupChatService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;
        private readonly NavigationManager _navigationManager;

        public ClientGroupChatService(HttpClient httpClient, ISnackbar snackbar, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
            _navigationManager = navigationManager;
        }
        public Task<bool> AddUserToGroup(int userId, int groupChatId)
        {
            throw new NotImplementedException();

        }

        public async Task<List<GroupChat>> CreateGroupChat(GroupChatDTO request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/GroupChat/create-group", request);

            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add(
                    "Group chat created successfully",
                    Severity.Success,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return await response.Content.ReadFromJsonAsync<List<GroupChat>>();
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                _snackbar.Add(
                    "Group chat already exists",
                    Severity.Warning,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return null;
            }
            else
            {
                // Handle other error cases if needed
                _snackbar.Add(
                    "Error creating group chat",
                    Severity.Error,
                    config =>
                    {
                        config.ShowTransitionDuration = 200;
                        config.HideTransitionDuration = 400;
                        config.VisibleStateDuration = 2500;
                    });

                return null;
            }
        }


        public async Task<List<GroupChat>> GetAllGroup()
        {
            return await _httpClient.GetFromJsonAsync<List<GroupChat>>($"api/GroupChat/all-groups/");

        }

        public async Task<List<GroupChatMessage>> GetConversationAsync(int groupChatId)
        {
            return await _httpClient.GetFromJsonAsync<List<GroupChatMessage>>($"api/GroupChat/get-convo/{groupChatId}");

        }

        public async Task<List<User>> GetGroupChatMembers(int groupChatId)
        {
            var result = await _httpClient.GetAsync($"api/GroupChat/users-from-group/{groupChatId}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var users = await result.Content.ReadFromJsonAsync<List<User>>();
                return users;
            }
           
            return new List<User>();
        }

        public async Task<string> GetGroupName(int groupChatId)
        {
            var response = await _httpClient.GetAsync($"api/GroupChat/get-group-name/{groupChatId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Handle 404 Not Found
                return null;
            }
            else
            {
                // Handle other error cases
                throw new Exception($"Error getting group name. Status code: {response.StatusCode}");
            }
        }


        public async Task<User> GetUserDetailsAsync(int userId)
        {
            var result = await _httpClient.GetAsync($"api/GroupChat/user-detail/{userId}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<User>>("api/GroupChat/users");
            return data;
        }

        public Task<List<GroupChat>> RemoveGroupChat(int groupChatId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserToGroup(int userId, int groupChatId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveMessagesAsync(GroupChatMessage message)
        {
            await _httpClient.PostAsJsonAsync("api/GroupChat", message);
        }
    }
}
