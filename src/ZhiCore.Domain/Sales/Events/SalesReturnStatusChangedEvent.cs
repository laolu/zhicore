using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales.Events;

public class SalesReturnStatusChangedEvent : DomainEvent
{
    public string ReturnNumber { get; }
    public SalesReturnStatus OldStatus { get; }
    public SalesReturnStatus NewStatus { get; }
    public DateTime ChangedTime { get; }

    public SalesReturnStatusChangedEvent(
        string returnNumber,
        SalesReturnStatus oldStatus,
        SalesReturnStatus newStatus)
    {
        ReturnNumber = returnNumber;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedTime = DateTime.Now;
    }
}