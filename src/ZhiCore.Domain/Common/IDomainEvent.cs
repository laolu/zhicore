namespace ZhiCore.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}