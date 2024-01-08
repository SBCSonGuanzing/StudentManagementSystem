using Microsoft.AspNetCore.Components;
using StudentSystem.Client.Pages;
using StudentSystem.Shared.Models;
using System.Net.Http.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace StudentSystem.Client.Services.AnnouncementServices
{
    public class ClientAnnouncementService : IClientAnnouncementService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;


        public ClientAnnouncementService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Announcement> announcements { get; set; } = new List<Announcement>();
        public async Task<List<Announcement>> GetAllAnnoucements()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Announcement>>($"api/Announcement");
            if (result != null)
            {
                announcements = result;
            }
            return result;
        }
        public async Task<List<Announcement>> AddAnnouncement(Announcement context)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Announcement>>($"api/Announcement");
            if (result != null)
            {
                announcements = result;
            }
            return result;
        }

      
      
    }
}
