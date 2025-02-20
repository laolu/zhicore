using My.ZhiCore.NotificationManagement.Notifications.Dtos;

namespace My.ZhiCore.NotificationManagement.Notifications;

public class NotificationSubscriptionManager : NotificationManagementDomainService, INotificationSubscriptionManager
{
    private readonly INotificationSubscriptionRepository _notificationSubscriptionRepository;

    public NotificationSubscriptionManager(INotificationSubscriptionRepository notificationSubscriptionRepository)
    {
        _notificationSubscriptionRepository = notificationSubscriptionRepository;
    }

    public async Task SetReadAsync(Guid receiveUserId, string receiveUserName, Guid notificationId)
    {
        var subscription = await _notificationSubscriptionRepository.FindAsync(receiveUserId, notificationId);
        if (subscription != null)
        {
            return;
        }

        subscription = new NotificationSubscription(GuidGenerator.Create(), notificationId, receiveUserId, receiveUserName, Clock.Now, true, CurrentTenant?.Id);
        await _notificationSubscriptionRepository.InsertAsync(subscription);
    }

    public async Task<List<NotificationSubscriptionDto>> GetPagingListAsync(Guid notificationId, Guid? receiverUserId, string receiverUserName, DateTime? startReadTime, DateTime? endReadTime, int maxResultCount = 10, int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var list = await _notificationSubscriptionRepository.GetPagingListAsync(notificationId, receiverUserId, receiverUserName, startReadTime, endReadTime, maxResultCount, skipCount, cancellationToken);
        return ObjectMapper.Map<List<NotificationSubscription>, List<NotificationSubscriptionDto>>(list);
    }

    public async Task<List<NotificationSubscriptionDto>> GetListAsync(List<Guid> notificationId, Guid receiverUserId, CancellationToken cancellationToken = default)
    {
        var list = await _notificationSubscriptionRepository.GetListAsync(notificationId, receiverUserId, cancellationToken);
        return ObjectMapper.Map<List<NotificationSubscription>, List<NotificationSubscriptionDto>>(list);
    }

    public async Task<NotificationSubscriptionDto> FindAsync(Guid receiveUserId, Guid notificationId, CancellationToken cancellationToken = default)
    {
        var subscription = await _notificationSubscriptionRepository.FindAsync(receiveUserId, notificationId, cancellationToken);
        return ObjectMapper.Map<NotificationSubscription, NotificationSubscriptionDto>(subscription);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var subscription = await _notificationSubscriptionRepository.FindAsync(id, false, cancellationToken);
        if (subscription == null) throw new NotificationManagementDomainException(NotificationManagementErrorCodes.MessageNotExist);
        await _notificationSubscriptionRepository.DeleteAsync(subscription.Id, false, cancellationToken);
    }

    public async Task<long> GetPagingCountAsync(Guid notificationId, Guid? receiverUserId, string receiverUserName, DateTime? startReadTime, DateTime? endReadTime, CancellationToken cancellationToken = default)
    {
        return await _notificationSubscriptionRepository.GetPagingCountAsync(notificationId, receiverUserId, receiverUserName, startReadTime, endReadTime, cancellationToken);
    }
}