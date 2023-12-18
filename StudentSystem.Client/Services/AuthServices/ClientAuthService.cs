using Microsoft.AspNetCore.Components;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace StudentSystem.Client.Services.AuthServices
{
    public class ClientAuthService : IClientAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public ClientAuthService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }
        public Token token { get; set; } = new Token();
        public List<User> users { get; set; } = new List<User>();

        public async Task<List<User>> GetAllUser()
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>($"api/Auth");
            if (result != null)
            {
                users = result;
            }
            return result;
        }

        public async Task<string> Login(LoginDTO request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Auth/login", request);
            var data = await SetToken(result);
            return data;
        }

        public async Task Register(UserDTO request)
        {
            await _httpClient.PostAsJsonAsync("api/Auth/register", request);
            _navigationManager.NavigateTo("/");
        }
        private async Task<string> SetToken(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                token.Value = await message.Content.ReadAsStringAsync();
                return "success";
            }
            else
            {
                return await message.Content.ReadAsStringAsync();
            }
        }
    }
}
