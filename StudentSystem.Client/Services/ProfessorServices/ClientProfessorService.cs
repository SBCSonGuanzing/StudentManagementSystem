using StudentSystem.Shared.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;

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

        public async Task<Professor> GetSingleProfessor(int id)
        {
            var result = await _httpClient.GetAsync($"api/Professor/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Professor>();
            }
            return null;    
        }

        public async Task UpdateProfessor(int id, Professor professor)
        {
            await _httpClient.PutAsJsonAsync($"api/Professor/update-professor/{id}", professor);
            _navigationManager.NavigateTo("/all-professors");
        }
    }
}
