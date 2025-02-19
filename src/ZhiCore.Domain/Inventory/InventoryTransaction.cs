using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryTransaction : AuditableEntity
{
    public int InventoryItemId { get; private set; }
    public TransactionType Type { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public string ReferenceNumber { get; private set; }
    public string Notes { get; private set; }
    public DateTime TransactionDate { get; private set; }

    private InventoryTransaction() { }

    public static InventoryTransaction Create(
        int inventoryItemId,
        TransactionType type,
        int quantity,
        decimal unitCost,
        string referenceNumber,
        string notes = null)
    {
        if (inventoryItemId <= 0)
            throw new ArgumentException("库存项ID必须大于0", nameof(inventoryItemId));

        if (quantity <= 0)
            throw new ArgumentException("交易数量必须大于0", nameof(quantity));

        if (unitCost < 0)
            throw new ArgumentException("单位成本不能为负数", nameof(unitCost));

        if (string.IsNullOrWhiteSpace(referenceNumber))
            throw new ArgumentException("引用单号不能为空", nameof(referenceNumber));

        return new InventoryTransaction
        {
            InventoryItemId = inventoryItemId,
            Type = type,
            Quantity = quantity,
            UnitCost = unitCost,
            ReferenceNumber = referenceNumber,
            Notes = notes,
            TransactionDate = DateTime.Now
        };
    }
}

public enum TransactionType
{
    StockIn = 0,        // 入库
    StockOut = 1,       // 出库
    Adjustment = 2,     // 调整
    Transfer = 3,       // 调拨
    Return = 4          // 退货
}