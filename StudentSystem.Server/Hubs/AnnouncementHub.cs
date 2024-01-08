using Microsoft.AspNetCore.SignalR;

namespace StudentSystem.Server.Hubs
{
    public class AnnouncementHub : Hub
    {
        public async Task SendAnnouncement(string Message)
        {
            await Clients.All.SendAsync($"ReceiveAnnouncement", Message);
        }
    }
}
