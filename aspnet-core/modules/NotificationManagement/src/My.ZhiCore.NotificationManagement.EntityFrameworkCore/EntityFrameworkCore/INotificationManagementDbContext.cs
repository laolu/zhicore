namespace My.ZhiCore.NotificationManagement.EntityFrameworkCore
{
    [ConnectionStringName(NotificationManagementDbProperties.ConnectionStringName)]
    public interface INotificationManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        
        DbSet<Notification> Notifications { get; set; }
        
        DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }
    }
}