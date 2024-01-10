using Microsoft.AspNetCore.SignalR;

namespace StudentSystem.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(ChatMessage message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }


    }
}
