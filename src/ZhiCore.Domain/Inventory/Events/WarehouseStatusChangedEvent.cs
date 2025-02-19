using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory.Events;

public class WarehouseStatusChangedEvent : DomainEvent
{
    public Guid WarehouseId { get; }
    public string WarehouseCode { get; }
    public bool OldStatus { get; }
    public bool NewStatus { get; }
    public DateTime ChangedTime { get; }

    public WarehouseStatusChangedEvent(
        Guid warehouseId,
        string warehouseCode,
        bool oldStatus,
        bool newStatus)
    {
        WarehouseId = warehouseId;
        WarehouseCode = warehouseCode;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedTime = DateTime.Now;
    }
}