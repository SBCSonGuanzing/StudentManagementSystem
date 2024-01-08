using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.AnnouncementServices
{
    public interface IAnnouncementService
    {
        Task <List<Announcement>> GetAllAnnouncements ();
        Task <List<Announcement>> AddAnnouncement(Announcement context);

    }
}
