using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryCheck : AuditableEntity
{
    public string CheckNumber { get; private set; }
    public string LocationCode { get; private set; }
    public CheckStatus Status { get; private set; }
    public DateTime CheckDate { get; private set; }
    public string Notes { get; private set; }
    private readonly List<InventoryCheckItem> _checkItems = new();
    public IReadOnlyList<InventoryCheckItem> CheckItems => _checkItems.AsReadOnly();

    private InventoryCheck() { }

    public static InventoryCheck Create(
        string checkNumber,
        string locationCode,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(checkNumber))
            throw new ArgumentException("盘点单号不能为空", nameof(checkNumber));

        if (string.IsNullOrWhiteSpace(locationCode))
            throw new ArgumentException("库位编码不能为空", nameof(locationCode));

        return new InventoryCheck
        {
            CheckNumber = checkNumber,
            LocationCode = locationCode,
            Status = CheckStatus.Draft,
            CheckDate = DateTime.Now,
            Notes = notes
        };
    }

    public void AddCheckItem(
        int inventoryItemId,
        int systemQuantity,
        int actualQuantity)
    {
        if (Status != CheckStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的盘点单可以添加盘点项");

        if (inventoryItemId <= 0)
            throw new ArgumentException("库存项ID必须大于0", nameof(inventoryItemId));

        if (systemQuantity < 0)
            throw new ArgumentException("系统数量不能为负数", nameof(systemQuantity));

        if (actualQuantity < 0)
            throw new ArgumentException("实际数量不能为负数", nameof(actualQuantity));

        var checkItem = new InventoryCheckItem(
            inventoryItemId,
            systemQuantity,
            actualQuantity);

        _checkItems.Add(checkItem);
    }

    public void Submit()
    {
        if (Status != CheckStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的盘点单可以提交");

        if (_checkItems.Count == 0)
            throw new InvalidOperationException("盘点单必须包含至少一个盘点项");

        Status = CheckStatus.Submitted;
    }

    public void Confirm()
    {
        if (Status != CheckStatus.Submitted)
            throw new InvalidOperationException("只有已提交状态的盘点单可以确认");

        Status = CheckStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status != CheckStatus.Draft && Status != CheckStatus.Submitted)
            throw new InvalidOperationException("只有草稿或已提交状态的盘点单可以取消");

        Status = CheckStatus.Cancelled;
    }
}

public class InventoryCheckItem
{
    public int InventoryItemId { get; private set; }
    public int SystemQuantity { get; private set; }
    public int ActualQuantity { get; private set; }
    public int Difference => ActualQuantity - SystemQuantity;

    internal InventoryCheckItem(
        int inventoryItemId,
        int systemQuantity,
        int actualQuantity)
    {
        InventoryItemId = inventoryItemId;
        SystemQuantity = systemQuantity;
        ActualQuantity = actualQuantity;
    }
}

public enum CheckStatus
{
    Draft = 0,      // 草稿
    Submitted = 1,  // 已提交
    Confirmed = 2,  // 已确认
    Cancelled = 3   // 已取消
}