using My.ZhiCore.NotificationManagement.Notifications.Dtos;

namespace My.ZhiCore.NotificationManagement
{
    public class NotificationDomainAutoMapperProfile:Profile
    {
        public NotificationDomainAutoMapperProfile()
        {
            CreateMap<Notification, NotificationEto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationSubscription, NotificationSubscriptionDto>();
        }
    }
}