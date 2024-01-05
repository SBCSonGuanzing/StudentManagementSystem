using Microsoft.AspNetCore.Components;
using StudentSystem.Shared.Models;
using System.Net.Http.Json;
using System.Net;
using StudentSystem.Shared.DTOs;
using MudBlazor;
using Blazorise.Snackbar;

namespace StudentSystem.Client.Services.SubjectServices
{
    public class ClientSubjectService : IClientSubjectService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public ClientSubjectService(HttpClient httpClient, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }

        public List<Subject> subjects { get; set; } = new List<Subject>();

        public async Task<int> AddSubject(SubjectDTO subject)
        {
            HttpResponseMessage? status = await _httpClient.PostAsJsonAsync("api/Subject", subject);

            if (status.IsSuccessStatusCode)
            {
            _snackbar.Add(
                        "Successfully Added Subject",
                        Severity.Success,
                        config =>
                        {
                            config.ShowTransitionDuration = 200;
                            config.HideTransitionDuration = 400;
                            config.VisibleStateDuration = 2500;
                        });
            _navigationManager.NavigateTo("/all-subjects");

            }
            else
            {
                _snackbar.Add(
                   "Already Existing Subject",
                   Severity.Warning,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });
                return 0;
            }



            return 0;
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

        public async Task UpdateSubject(int id, SubjectDTO subject)
        {
            await _httpClient.PutAsJsonAsync($"api/Subject/update-subject/{id}", subject);
            _navigationManager.NavigateTo("/all-subjects");
        }
    }
}
