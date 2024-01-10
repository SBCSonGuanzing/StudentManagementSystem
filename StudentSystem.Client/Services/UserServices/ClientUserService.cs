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

        public List<EnrolledSubjects> professorStudents { get; set; } = new List<EnrolledSubjects>();

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

        public async Task<Student?> GetSingleStudent()
        {
            var result = await _httpClient.GetAsync($"api/User/single-student");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<Student>();
            }
            return null;
        }
        
     
        public async Task<string> GetUserRole()
        {
            var result = await _httpClient.GetStringAsync("api/User/user-role");
            return result;
        }

        public async Task<Professor?> GetSingleProfessor()
        {
            var result = await _httpClient.GetAsync($"api/User/single-professor");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<Professor>();
            }
            return null;
        }
    }
}
