using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace StudentSystem.Client.Services.GroupChatServices
{
    public class ClientGroupChatService : IClientGroupChatService
    {
        private readonly HttpClient _httpClient;

        public ClientGroupChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<bool> AddUserToGroup(int userId, int groupChatId)
        {
            throw new NotImplementedException();

        }

        public Task<List<GroupChat>> CreateGroupChat(GroupChatDTO request)
        {
            throw new NotImplementedException();
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


        public Task<User> GetUserDetailsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupChat>> RemoveGroupChat(int groupChatId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserToGroup(int userId, int groupChatId)
        {
            throw new NotImplementedException();
        }

        public Task SaveMessagesAsyc(GroupChatMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
