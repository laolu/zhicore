using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales.Events;

public class SalesOrderStatusChangedEvent : DomainEvent
{
    public string OrderNumber { get; }
    public SalesOrderStatus OldStatus { get; }
    public SalesOrderStatus NewStatus { get; }
    public DateTime ChangedTime { get; }

    public SalesOrderStatusChangedEvent(
        string orderNumber,
        SalesOrderStatus oldStatus,
        SalesOrderStatus newStatus)
    {
        OrderNumber = orderNumber;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedTime = DateTime.Now;
    }
}