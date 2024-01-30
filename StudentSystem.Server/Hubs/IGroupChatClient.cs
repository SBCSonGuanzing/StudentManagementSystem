using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Hubs
{
    public interface IGroupChatClient
    {
        Task ReceiveMessage(ChatMessage message, string userName);
        Task ReceiveChatNotification(string message, int receiverUserId, string senderUserId);
        Task ReceivedGroupName(GroupChat group);
        Task ReceiveUserToAdd(User request);
        Task ReceiveGroupToUpdate(int groupId, GroupToUpdate request);
        Task ReceiveGroupToRemove(int groupChatId);
        Task ReceiveUserToRemove(string userId);
        Task ReceivedGroupMessage(GroupChatMessage message);
        Task getProfileInfo(User user);
        Task GetEmail(string email);

    }
}
