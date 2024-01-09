using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.AnnouncementServices;
using StudentSystem.Shared.Models;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService AnnouncementService)
        {
            _announcementService = AnnouncementService;
        }

        [HttpPost]
        public async Task<ActionResult<List<Announcement>>> AddAnnouncement(Announcement message)
        {
            var result = await _announcementService.AddAnnouncement(message);
            return Ok(result);

        }

        [HttpGet]
        public async Task<ActionResult<List<Announcement>>> GetAllAnnouncements()
        {
            var result = await _announcementService.GetAllAnnouncements();
            return Ok(result);
        }


    }
}
