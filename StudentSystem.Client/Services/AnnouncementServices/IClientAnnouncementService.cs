using StudentSystem.Client.Pages;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.AnnouncementServices
{
    public interface IClientAnnouncementService
    {
        List<Announcement> announcements { get; set; }
        Task<List<Announcement>> GetAllAnnoucements(); 
        Task<List<Announcement>> AddAnnouncement(Announcement context);
    }
}
