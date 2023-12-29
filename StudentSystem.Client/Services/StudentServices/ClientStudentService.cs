using Microsoft.AspNetCore.Components;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net.Http.Json;
using System.Net;

namespace StudentSystem.Client.Services.StudentServices
{
    public class ClientStudentService : IClientStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public ClientStudentService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Student> students { get; set; } = new List<Student>();

        public async Task<List<Student>> GetAllStudents()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Student>>($"api/Student");
            if (result != null)
            {
                students = result;
            }
            return result;
        }

        public async Task<Student> GetSingleStudent(int id)
        {
            var result = await _httpClient.GetAsync($"api/Student/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Student>();
            }
            return null;
        }

        public async Task UpdateStudent(int id, Student student)
        {
            await _httpClient.PutAsJsonAsync($"api/Student/update-student/{id}", student);
            _navigationManager.NavigateTo("/all-students");
        }
    }
}
