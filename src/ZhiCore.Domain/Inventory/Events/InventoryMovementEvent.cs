using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory.Events;

public class InventoryMovementEvent : DomainEvent
{
    public int InventoryItemId { get; }
    public string SourceLocationCode { get; }
    public string DestinationLocationCode { get; }
    public int Quantity { get; }
    public string BatchNumber { get; }
    public string ReferenceNumber { get; }
    public string OperatorId { get; }
    public DateTime MovementTime { get; }
    
    public InventoryMovementEvent(
        int inventoryItemId,
        string sourceLocationCode,
        string destinationLocationCode,
        int quantity,
        string batchNumber,
        string referenceNumber,
        string operatorId)
    {
        InventoryItemId = inventoryItemId;
        SourceLocationCode = sourceLocationCode;
        DestinationLocationCode = destinationLocationCode;
        Quantity = quantity;
        BatchNumber = batchNumber;
        ReferenceNumber = referenceNumber;
        OperatorId = operatorId;
        MovementTime = DateTime.Now;
    }
}