using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Client.Services.GroupChatServices;
using StudentSystem.Client.Services.UserServices;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using System.Security.Claims;

namespace StudentSystem.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinGroup(string group)
        {           
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessage(ChatMessage message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }

        public async Task ChatNotificationAsync(string message, int receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        }

        public async Task CreateGroup(GroupChat group)
        {
            await Clients.All.SendAsync("ReceivedGroupName", group);
        }     

        public async Task AddToGroup(string groupName, User request)
        {
            await Clients.Group(groupName).SendAsync("ReceiveUserToAdd", request);
        }

        public async Task RemoveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, GroupChatMessage message)
        {
            // Append the message to the group chat
            await Clients.Group(groupName).SendAsync("ReceivedGroupMessage", message);
        }
    }
}