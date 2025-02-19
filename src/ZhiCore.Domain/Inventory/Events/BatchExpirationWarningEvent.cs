using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory.Events;

public class BatchExpirationWarningEvent : DomainEvent
{
    public int InventoryItemId { get; }
    public string BatchNumber { get; }
    public DateTime ExpiryDate { get; }
    public int RemainingDays { get; }
    public int CurrentQuantity { get; }
    
    public BatchExpirationWarningEvent(
        int inventoryItemId,
        string batchNumber,
        DateTime expiryDate,
        int remainingDays,
        int currentQuantity)
    {
        InventoryItemId = inventoryItemId;
        BatchNumber = batchNumber;
        ExpiryDate = expiryDate;
        RemainingDays = remainingDays;
        CurrentQuantity = currentQuantity;
    }
}