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
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentSystem.Server.Hubs
{
    public class ChatHub : Hub<IGroupChatClient>
    {
        private Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

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
      
        public async Task RemoveGroup(string groupName)
        {
            
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task GroupNameUpdated(int groupId, GroupToUpdate request)
        {
            await Clients.All.ReceiveGroupToUpdate(groupId, request);
        }

        public async Task RemoveGroupAsync(int groupChatId, string groupName)
        {
            await Clients.Group(groupName).ReceiveGroupToRemove(groupChatId); 
        }
 
        public async Task RemoveUserGroup(string groupName, string userId)
        {
            var foo = getEmail();
            try
            {
                var connectionIdToRemove = _ConnectionsMap.FirstOrDefault(x => x.Key == userId).Value;
                    Console.WriteLine ("Connection ID: ", connectionIdToRemove);
                if(!string.IsNullOrEmpty(connectionIdToRemove))
                {
                    _ConnectionsMap.Remove(userId.ToString());

                    // Disconnect the user from the group
                    await Groups.RemoveFromGroupAsync(connectionIdToRemove, groupName);
                    
                    // Notify the group about the user removal
                    await Clients.Group(groupName).ReceiveUserToRemove(userId);
                }
                else
                {
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
        }


        public async Task GetEmail(string email)
        {
            // Store the email in the user's context
            userEmail = email;

            // Notify clients about the received email
            await Clients.All.GetEmail(email);
        }
        public override async Task OnConnectedAsync()
        {

            Console.WriteLine($"User connected with email: {userEmail}");


            //try
            //{
            //    var user = GetEmail();
            //    var userDetails = await _context.Users.Where(m => m.Email == user).FirstOrDefaultAsync();

            //    if (userDetails != null && !_ConnectionsMap.ContainsKey(userDetails.Id.ToString()))
            //    {
            //        _ConnectionsMap.Add(userDetails.Id.ToString(), Context.ConnectionId);
            //    }

            //    await Clients.Caller.getProfileInfo(userDetails);
            //}
            //catch (Exception ex)
            //{
            //    //await Clients.Caller.SendAsync("onError", "OnConnected: " + ex.Message);
            //}

            await base.OnConnectedAsync();
        }

    

        private string? IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

    }
} 