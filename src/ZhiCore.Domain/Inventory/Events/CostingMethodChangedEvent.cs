using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory.Events;

public class CostingMethodChangedEvent : DomainEvent
{
    public int InventoryItemId { get; }
    public CostingMethod OldMethod { get; }
    public CostingMethod NewMethod { get; }
    public decimal OldAverageCost { get; }
    public decimal NewAverageCost { get; }
    public string Reason { get; }
    public DateTime ChangedTime { get; }
    
    public CostingMethodChangedEvent(
        int inventoryItemId,
        CostingMethod oldMethod,
        CostingMethod newMethod,
        decimal oldAverageCost,
        decimal newAverageCost,
        string reason)
    {
        InventoryItemId = inventoryItemId;
        OldMethod = oldMethod;
        NewMethod = newMethod;
        OldAverageCost = oldAverageCost;
        NewAverageCost = newAverageCost;
        Reason = reason;
        ChangedTime = DateTime.Now;
    }
}