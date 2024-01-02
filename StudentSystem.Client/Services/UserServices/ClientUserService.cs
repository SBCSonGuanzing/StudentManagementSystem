﻿
using StudentSystem.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace StudentSystem.Client.Services.UserServices
{
    public class ClientUserService : IClientUserService
    {
        private readonly HttpClient _httpClient;

        public ClientUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<User> users { get; set; } = new List<User>();

        public async Task<List<User>> DeleteUser(int id)
        {
            HttpResponseMessage? response = await _httpClient.DeleteAsync($"api/User/{id}");

            if (response != null && response.IsSuccessStatusCode)
            {
                List<User>? content = await response.Content.ReadFromJsonAsync<List<User>>();
                if (content != null)
                {
                    users = content;
                }
            }
            return users;
        }

        public async Task<List<User>> GetAllUser()
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>($"api/User");
            if (result != null)
            {
                users = result;
            }
            return result;
        }

        public async Task<string> GetSingleUser()
        {
            var result = await _httpClient.GetStringAsync("api/User/single-avatar");
            return result;
        }

        public async Task<int> GetSingleStudent(int id)
        {
            var result = await _httpClient.GetAsync($"api/User/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<int>();
            }
            return 0;
        }

        public async Task<string> GetUserRole()
        {
            var result = await _httpClient.GetStringAsync("api/User/user-role");
            return result;
        }
    }
}
