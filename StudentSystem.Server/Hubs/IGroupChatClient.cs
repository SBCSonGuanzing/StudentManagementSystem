namespace StudentSystem.Server.Hubs
{
    public interface IGroupChatClient
    {
        Task ReceiveMessage(ChatMessage message, string userName);
        Task ReceiveChatNotification(string message, int receiverUserId, string senderUserId);
        Task ReceivedGroupName(GroupChat group);
        Task ReceiveUserToAdd(User request);
        Task ReceivedGroupMessage(GroupChatMessage message);
    }
}
