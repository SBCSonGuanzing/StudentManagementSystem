
using StudentSystem.Server.Data;
using StudentSystem.Shared.Models;

namespace StudentSystem.Server.Services.AnnouncementServices
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly DataContext _context;

        public AnnouncementService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Announcement>> AddAnnouncement(Announcement context)
        {
            Announcement newAnnouncement = new Announcement()
            {
                Message = context.Message,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Add(newAnnouncement);
            await _context.SaveChangesAsync();
            return await GetAllAnnouncements();
        }

        public async Task<List<Announcement>> GetAllAnnouncements()
        {
            var announcements = await _context.Announcements.ToListAsync();
            return announcements;
        }

     
    }
}
