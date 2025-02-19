using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory.Events;

public class StockStagnationWarningEvent : DomainEvent
{
    public int InventoryItemId { get; }
    public string LocationCode { get; }
    public int CurrentQuantity { get; }
    public int StagnationDays { get; }
    public decimal TotalValue { get; }
    public DateTime LastMovementDate { get; }
    
    public StockStagnationWarningEvent(
        int inventoryItemId,
        string locationCode,
        int currentQuantity,
        int stagnationDays,
        decimal totalValue,
        DateTime lastMovementDate)
    {
        InventoryItemId = inventoryItemId;
        LocationCode = locationCode;
        CurrentQuantity = currentQuantity;
        StagnationDays = stagnationDays;
        TotalValue = totalValue;
        LastMovementDate = lastMovementDate;
    }
}