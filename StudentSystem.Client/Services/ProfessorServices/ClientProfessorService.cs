using StudentSystem.Shared.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace StudentSystem.Client.Services.ProfessorServices
{
    public class ClientProfessorService : IClientProfessorService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public ClientProfessorService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }
        public List<Professor> professors { get; set; }

        public async Task<List<Professor>> GetAllProfessors()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Professor>>($"api/Professor");
            if (result != null)
            {
                professors = result;
            }
            return result;
        }
    }
}
