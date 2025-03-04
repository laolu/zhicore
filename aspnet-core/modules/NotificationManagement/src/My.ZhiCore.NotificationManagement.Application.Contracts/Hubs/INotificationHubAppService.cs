namespace My.ZhiCore.NotificationManagement.Hubs;

public interface INotificationHubAppService : IApplicationService
{
    Task SendMessageAsync(Guid id, string title, string content, MessageType messageType,MessageLevel messageLevel, string receiverUserId);
}