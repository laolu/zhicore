using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement.Events;

public class PurchaseOrderStatusChangedEvent : DomainEvent
{
    public string OrderNumber { get; }
    public PurchaseOrderStatus OldStatus { get; }
    public PurchaseOrderStatus NewStatus { get; }
    public DateTime ChangedTime { get; }

    public PurchaseOrderStatusChangedEvent(
        string orderNumber,
        PurchaseOrderStatus oldStatus,
        PurchaseOrderStatus newStatus)
    {
        OrderNumber = orderNumber;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedTime = DateTime.Now;
    }
}