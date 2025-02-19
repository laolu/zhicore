using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryAlert : AuditableEntity
{
    public int InventoryItemId { get; private set; }
    public AlertType Type { get; private set; }
    public AlertStatus Status { get; private set; }
    public int ThresholdQuantity { get; private set; }    // 预警阈值
    public DateTime? ExpiryThresholdDate { get; private set; }  // 有效期预警阈值
    public int DaysBeforeExpiry { get; private set; }    // 过期前多少天预警
    public decimal StagnationDays { get; private set; }  // 积压天数阈值
    public string Notes { get; private set; }

    private InventoryAlert() { }

    public static InventoryAlert CreateMinimumAlert(
        int inventoryItemId,
        int thresholdQuantity,
        string notes = null)
    {
        if (inventoryItemId <= 0)
            throw new ArgumentException("库存项ID必须大于0", nameof(inventoryItemId));

        if (thresholdQuantity <= 0)
            throw new ArgumentException("预警阈值必须大于0", nameof(thresholdQuantity));

        return new InventoryAlert
        {
            InventoryItemId = inventoryItemId,
            Type = AlertType.MinimumStock,
            Status = AlertStatus.Active,
            ThresholdQuantity = thresholdQuantity,
            Notes = notes
        };
    }

    public static InventoryAlert CreateExpiryAlert(
        int inventoryItemId,
        int daysBeforeExpiry,
        string notes = null)
    {
        if (inventoryItemId <= 0)
            throw new ArgumentException("库存项ID必须大于0", nameof(inventoryItemId));

        if (daysBeforeExpiry <= 0)
            throw new ArgumentException("过期前预警天数必须大于0", nameof(daysBeforeExpiry));

        return new InventoryAlert
        {
            InventoryItemId = inventoryItemId,
            Type = AlertType.Expiry,
            Status = AlertStatus.Active,
            DaysBeforeExpiry = daysBeforeExpiry,
            Notes = notes
        };
    }

    public static InventoryAlert CreateStagnationAlert(
        int inventoryItemId,
        decimal stagnationDays,
        string notes = null)
    {
        if (inventoryItemId <= 0)
            throw new ArgumentException("库存项ID必须大于0", nameof(inventoryItemId));

        if (stagnationDays <= 0)
            throw new ArgumentException("积压天数必须大于0", nameof(stagnationDays));

        return new InventoryAlert
        {
            InventoryItemId = inventoryItemId,
            Type = AlertType.Stagnation,
            Status = AlertStatus.Active,
            StagnationDays = stagnationDays,
            Notes = notes
        };
    }

    public void Deactivate()
    {
        if (Status != AlertStatus.Active)
            throw new InvalidOperationException("只有激活状态的预警可以停用");

        Status = AlertStatus.Inactive;
    }

    public void Activate()
    {
        if (Status != AlertStatus.Inactive)
            throw new InvalidOperationException("只有停用状态的预警可以激活");

        Status = AlertStatus.Active;
    }
}

public enum AlertType
{
    MinimumStock = 0,    // 最低库存预警
    Expiry = 1,          // 有效期预警
    Stagnation = 2       // 积压预警
}

public enum AlertStatus
{
    Active = 0,      // 激活
    Inactive = 1     // 停用
}