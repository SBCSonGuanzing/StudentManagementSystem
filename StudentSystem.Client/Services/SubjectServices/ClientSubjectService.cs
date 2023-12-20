using Microsoft.AspNetCore.Components;
using StudentSystem.Shared.Models;
using System.Net.Http.Json;
using System.Net;

namespace StudentSystem.Client.Services.SubjectServices
{
    public class ClientSubjectService : IClientSubjectService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public ClientSubjectService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Subject> subjects { get; set; } = new List<Subject>();

        public async Task AddSubject(Subject subject)
        {
            await _httpClient.PostAsJsonAsync("api/Subject", subject);
            _navigationManager.NavigateTo("/all-subjects");
        }

        public async Task<List<Subject>> DeleteSubject(int id)
        {
            HttpResponseMessage? response = await _httpClient.DeleteAsync($"api/Subject/delete-subject/{id}");

            if (response != null && response.IsSuccessStatusCode)
            {
                List<Subject>? content = await response.Content.ReadFromJsonAsync<List<Subject>>();
                if (content != null)
                {
                    subjects = content;
                }

            }
            return subjects;
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Subject>>($"api/Subject");
            if (result != null)
            {
                subjects = result;
            }
            return result;
        }

        public async Task<Subject> GetSingleSubject(int id)
        {
            var result = await _httpClient.GetAsync($"api/Subject/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Subject>();
            }
            return null;
        }

        public async Task UpdateSubject(int id, Subject subject)
        {
            await _httpClient.PutAsJsonAsync($"api/Subject/update-subject/{id}", subject);
            _navigationManager.NavigateTo("/all-subjects");
        }
    }
}
