namespace My.ZhiCore.NotificationManagement.Hubs
{
    [HubRoute("SignalR/Notification")]
    [Authorize]
    [DisableAuditing]
    public class NotificationHub : AbpHub<INotificationHub>
    {
        
    }
}