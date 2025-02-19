using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryTransfer : AuditableEntity
{
    public int SourceInventoryItemId { get; private set; }
    public int DestinationInventoryItemId { get; private set; }
    public int Quantity { get; private set; }
    public string TransferNumber { get; private set; }
    public TransferStatus Status { get; private set; }
    public string Notes { get; private set; }
    public DateTime TransferDate { get; private set; }

    private InventoryTransfer() { }

    public static InventoryTransfer Create(
        int sourceInventoryItemId,
        int destinationInventoryItemId,
        int quantity,
        string transferNumber,
        string notes = null)
    {
        if (sourceInventoryItemId <= 0)
            throw new ArgumentException("源库存项ID必须大于0", nameof(sourceInventoryItemId));

        if (destinationInventoryItemId <= 0)
            throw new ArgumentException("目标库存项ID必须大于0", nameof(destinationInventoryItemId));

        if (sourceInventoryItemId == destinationInventoryItemId)
            throw new ArgumentException("源库存项和目标库存项不能相同");

        if (quantity <= 0)
            throw new ArgumentException("调拨数量必须大于0", nameof(quantity));

        if (string.IsNullOrWhiteSpace(transferNumber))
            throw new ArgumentException("调拨单号不能为空", nameof(transferNumber));

        return new InventoryTransfer
        {
            SourceInventoryItemId = sourceInventoryItemId,
            DestinationInventoryItemId = destinationInventoryItemId,
            Quantity = quantity,
            TransferNumber = transferNumber,
            Status = TransferStatus.Pending,
            Notes = notes,
            TransferDate = DateTime.Now
        };
    }

    public void Confirm()
    {
        if (Status != TransferStatus.Pending)
            throw new InvalidOperationException("只有待确认状态的调拨单可以确认");

        Status = TransferStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status != TransferStatus.Pending)
            throw new InvalidOperationException("只有待确认状态的调拨单可以取消");

        Status = TransferStatus.Cancelled;
    }
}

public enum TransferStatus
{
    Pending = 0,    // 待确认
    Confirmed = 1,  // 已确认
    Cancelled = 2   // 已取消
}