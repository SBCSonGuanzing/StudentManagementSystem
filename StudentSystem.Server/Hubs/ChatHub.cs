using Microsoft.AspNetCore.SignalR;

namespace StudentSystem.Server.Hubs
{
    public class ChatHub : Hub
    {                                                                                                                                          
        public async Task SendMessage(ChatMessage message, string userName)
        {
            await Clients.All.SendAsync($"ReceiveMessage", message, userName);
        }
        public async Task ChatNotificationAsync(string message, int receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task RemoveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string group, string message)
        {
            await Clients.Group(group).SendAsync("ReceivedMessage", message);
        }
    }
}   