using StudentSystem.Shared.DTOs;
using System.Runtime.InteropServices;

namespace StudentSystem.Server.Hubs
{
    public interface IGroupChatClient
    {
        Task ReceiveMessage(ChatMessage message, string userName);
        Task ReceiveChatNotification(string message, int receiverUserId, string senderUserId);
        Task ReceiveGroupChatNotification(GroupChatMessage message);
        Task ReceivedGroupName(GroupChat group);
        Task ReceiveUserToAdd(User request);
        Task ReceiveGroupToUpdate(int groupId, GroupToUpdate request);
        Task ReceiveGroupToRemove(int groupChatId);
        Task ReceiveUserToRemove(User user);
        Task ReceivedGroupMessage(GroupChatMessage message);
        Task GetEmail(string email);
        Task ActiveStatus(string email);
        Task InActiveStatus(string email);
        Task Send(string message);
        Task RemoveMemberNotification(GroupChatMessage message, string userName);
    }


}
