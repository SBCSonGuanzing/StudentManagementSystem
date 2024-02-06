using Azure.Core;
using Blazorise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Client.Services.GroupChatServices;
using StudentSystem.Client.Services.UserServices;
using StudentSystem.Server.Data;
using StudentSystem.Server.Services.UserServices;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentSystem.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IGroupChatClient>
    {

        public readonly static List<User> _Connections = new List<User>();
        private static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public string userEmail;

        public ChatHub(DataContext context, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        private string? getEmail() => _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            Console.WriteLine($"User: {Context.User.Identity.Name} is connected to Group: {group}");
            await Clients.Group(group).ActiveStatus(Context.User.Identity.Name);
        }

        public async Task RemoveGroup(string groupName)
        {

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            Console.WriteLine($"User: {Context.User.Identity.Name} is disconnected to Group: {groupName}");
        }

        //TODO: Make a new Method to Change Status to Inactive

        public async Task ChangeStatus(string groupName)
        { 
            await Clients.Group(groupName).InActiveStatus(Context.User.Identity.Name);
        }

        public async Task SendMessage(ChatMessage message, string userName)
        {
            await Clients.All.ReceiveMessage(message, userName);

        }

        public async Task ChatNotificationAsync(string message, int receiverUserId, string senderUserId)
        {
            await Clients.All.ReceiveChatNotification(message, receiverUserId, senderUserId);

        }

       
        public async Task CreateGroup(GroupChat group)
        {
            await Clients.All.ReceivedGroupName(group);
        }     

        public async Task AddToGroup(string groupName, User request)
        {
            await Clients.Group(groupName).ReceiveUserToAdd(request);

        }

        public async Task GroupNameUpdated(int groupId, GroupToUpdate request)
        {
            await Clients.All.ReceiveGroupToUpdate(groupId, request);
        }

        public async Task RemoveGroupAsync(int groupChatId, string groupName)
        {
            await Clients.Group(groupName).ReceiveGroupToRemove(groupChatId); 
        }
 
        public async Task RemoveUserGroup(string groupName, User user)
        {
            var foo = getEmail();

            try
            {
                var connectionIdToRemove = _ConnectionsMap.FirstOrDefault(x => x.Key == user.Email).Value;
                 
                if(!string.IsNullOrEmpty(connectionIdToRemove))
                { 
                    await Clients.Group(groupName).ReceiveUserToRemove(user);
                    // Disconnect the user from the group
                    await Groups.RemoveFromGroupAsync(connectionIdToRemove, groupName);
                    
                }
                else
                {
                    await Clients.OthersInGroup(groupName).ReceiveUserToRemove(user);

                    //await Clients.Caller.SendAsync("onError", "User not found for removal from group");
                }

            } catch (Exception ex)
            {
                //await Clients.Caller.SendAsync("onError", "RemoveGroupAsync: " + ex.Message);
            }
        }

        public async Task SendMessageToGroup(string groupName, GroupChatMessage message)
        {
            await Clients.Group(groupName).ReceivedGroupMessage(message);
            await GroupChatNotification(groupName, message);
        }

        public async Task GroupChatNotification(string group, GroupChatMessage message)
        {
            await Clients.OthersInGroup(group).ReceiveGroupChatNotification(message);
        }


        public override async Task OnConnectedAsync()
        {
            try
            {
                var Email = IdentityName;

                var user = _context.Users.Where(u => u.Email == IdentityName).FirstOrDefault();

            if(!_ConnectionsMap.ContainsKey(user.Email) && user != null)
            {
                _Connections.Add(user);
                _ConnectionsMap.Add(Email, Context.ConnectionId);

                Console.WriteLine(Email);
                Console.WriteLine($"User connected with ConnectionId: {Context.ConnectionId}");
                Console.WriteLine($"Connections: {_ConnectionsMap.Count}");
            }

                //await Clients.All.ReceiveGroupToUpdate();
            }
            catch (Exception ex)
            {
                //await Clients.Caller.SendAsync("onError", "OnConnected: " + ex.Message);
            }

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = _Connections.Where(u => u.Email == IdentityName).First();
                _Connections.Remove(user);

                // Tell other users to remove you from their list
                //Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                //// Remove mapping
                _ConnectionsMap.Remove(user.Email);
            }
            catch (Exception ex)
            {
                //Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }

        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

    }
} 