namespace ZhiCore.Domain.Common;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
}