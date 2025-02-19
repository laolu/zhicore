using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryItem : AuditableEntity
{
    public int ProductId { get; private set; }
    public string LocationCode { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public string BatchNumber { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public InventoryStatus Status { get; private set; }

    private InventoryItem() { }

    public static InventoryItem Create(
        int productId,
        string locationCode,
        int quantity,
        decimal unitCost,
        string batchNumber = null,
        DateTime? expiryDate = null)
    {
        if (productId <= 0)
            throw new ArgumentException("商品ID必须大于0", nameof(productId));

        if (string.IsNullOrWhiteSpace(locationCode))
            throw new ArgumentException("库位编码不能为空", nameof(locationCode));

        if (quantity < 0)
            throw new ArgumentException("库存数量不能为负数", nameof(quantity));

        if (unitCost < 0)
            throw new ArgumentException("单位成本不能为负数", nameof(unitCost));

        return new InventoryItem
        {
            ProductId = productId,
            LocationCode = locationCode,
            Quantity = quantity,
            UnitCost = unitCost,
            BatchNumber = batchNumber,
            ExpiryDate = expiryDate,
            Status = InventoryStatus.Available
        };
    }

    public void AddStock(int quantity, decimal unitCost)
    {
        if (quantity <= 0)
            throw new ArgumentException("入库数量必须大于0", nameof(quantity));

        if (unitCost < 0)
            throw new ArgumentException("单位成本不能为负数", nameof(unitCost));

        Quantity += quantity;
        // 使用加权平均法计算新的单位成本
        UnitCost = ((UnitCost * (Quantity - quantity)) + (unitCost * quantity)) / Quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("出库数量必须大于0", nameof(quantity));

        if (quantity > Quantity)
            throw new InvalidOperationException("库存不足");

        Quantity -= quantity;

        if (Quantity == 0)
        {
            Status = InventoryStatus.OutOfStock;
        }
    }

    public void Hold()
    {
        if (Status != InventoryStatus.Available)
            throw new InvalidOperationException("只有可用状态的库存可以冻结");

        Status = InventoryStatus.OnHold;
    }

    public void Release()
    {
        if (Status != InventoryStatus.OnHold)
            throw new InvalidOperationException("只有冻结状态的库存可以解冻");

        Status = InventoryStatus.Available;
    }
}

public enum InventoryStatus
{
    Available = 0,
    OutOfStock = 1,
    OnHold = 2
}