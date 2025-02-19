using Microsoft.Extensions.DependencyInjection;

namespace ZhiCore.Domain.Common;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAndClearEvents(IAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (dynamic handler in handlers)
            {
                await handler.Handle((dynamic)domainEvent, cancellationToken);
            }
        }

        aggregateRoot.ClearDomainEvents();
    }
}