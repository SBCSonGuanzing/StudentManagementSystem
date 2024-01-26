using Azure.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Client.Services.GroupChatServices;
using StudentSystem.Client.Services.UserServices;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentSystem.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        private readonly DataContext _context;

        public ChatHub(DataContext context)
        {
            _context = context;
        }

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

        public async Task GroupNameUpdated(int groupId, GroupToUpdate request)
        {
            await Clients.All.SendAsync("ReceiveGroupToUpdate", groupId, request);
        }

        public async Task RemoveGroupAsync(int groupChatId, string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupToRemove", groupChatId); 
        }
 
        public async Task RemoveUserGroup(string groupName, int userId)
        {
            try
            {
                var connectionIdToRemove = _ConnectionsMap.FirstOrDefault(x => x.Key == userId.ToString()).Value;
                if(!string.IsNullOrEmpty(connectionIdToRemove))
                {
                    _ConnectionsMap.Remove(userId.ToString());

                    // Disconnect the user from the group
                    await Groups.RemoveFromGroupAsync(connectionIdToRemove, groupName);
                    
                    // Notify the group about the user removal
                    await Clients.Group(groupName).SendAsync("ReceiveUserToRemove", userId);
                }
                else
                {
                    await Clients.Caller.SendAsync("onError", "User not found for removal from group");
                }

            } catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "RemoveGroupAsync: " + ex.Message);
            }
        }

        public async Task SendMessageToGroup(string groupName, GroupChatMessage message)
        {
            await Clients.Group(groupName).SendAsync("ReceivedGroupMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var user = _context.Users.Where(u => u.Email == IdentityName).FirstOrDefault();
                var userViewModel = _context.Users.Where(m => m.Email == IdentityName).FirstOrDefault();

                if (userViewModel != null && !_ConnectionsMap.ContainsKey(userViewModel.Id.ToString()))
                {
                    _ConnectionsMap.Add(userViewModel.Id.ToString(), Context.ConnectionId);
                }

                await Clients.Caller.SendAsync("getProfileInfo", userViewModel);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "OnConnected: " + ex.Message);
            }

            await base.OnConnectedAsync();
        }


        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

    }
} 