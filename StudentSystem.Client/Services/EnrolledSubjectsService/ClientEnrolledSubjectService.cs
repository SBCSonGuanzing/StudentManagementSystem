using Microsoft.AspNetCore.Components;
using MudBlazor;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace StudentSystem.Client.Services.EnrolledSubjectsService
{
    public class ClientEnrolledSubjectService : IClientEnrolledSubjectService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public ClientEnrolledSubjectService(HttpClient httpClient, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }

        public List<EnrolledSubjects> ClientEnrolledSubjects { get; set; } = new List<EnrolledSubjects>();

        
        public async Task<int> AddEnrolledSubject(EnrollmentDTO request)
        {
            HttpResponseMessage? status = await _httpClient.PostAsJsonAsync("api/EnrolledSubjects/add-enrolled-sub", request);
            _navigationManager.NavigateTo("/all-enrollment");

            if (status.IsSuccessStatusCode)
            {
                _snackbar.Add(
                   "Successfully Enrolled Subject",
                   Severity.Success,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });

                _navigationManager.NavigateTo("/all-enrollment");
            }
            else
            {
                _snackbar.Add(
                   "Already Enrolled Subject",
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

        public async Task<List<EnrolledSubjects>> GetAllEnrolledSubject()
        {
            var result = await _httpClient.GetFromJsonAsync<List<EnrolledSubjects>>("api/EnrolledSubjects/");
            if (result != null)
            {
                ClientEnrolledSubjects = result;
            }
            return ClientEnrolledSubjects;
        }

        public async Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<EnrolledSubjects>>($"api/EnrolledSubjects/{id}");
            if (result == null)
            {
                ClientEnrolledSubjects = result;
            }
            return result;
        }
    }
}
