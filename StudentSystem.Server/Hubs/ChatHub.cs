using Microsoft.AspNetCore.SignalR;

namespace StudentSystem.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string User, string Message)
        {
            await Clients.All.SendAsync($"ReceiveMessage", User, Message);
        }
    }
}
