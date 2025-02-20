namespace My.ZhiCore.NotificationManagement
{
    public abstract class NotificationManagementController : AbpController
    {
        protected NotificationManagementController()
        {
            LocalizationResource = typeof(NotificationManagementResource);
        }
    }
}
