namespace My.ZhiCore.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementApplicationModule),
        typeof(NotificationManagementDomainTestModule)
        )]
    public class NotificationManagementApplicationTestModule : AbpModule
    {

    }
}
