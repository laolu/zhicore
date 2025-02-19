using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class ProcurementReconciliation : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string SupplierCode { get; private set; }
    public string SupplierName { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public ReconciliationStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal ConfirmedAmount { get; private set; }
    public decimal DifferenceAmount { get; private set; }
    public string Description { get; private set; }

    private readonly List<ReconciliationItem> _items = new();
    public IReadOnlyCollection<ReconciliationItem> Items => _items.AsReadOnly();

    private ProcurementReconciliation() { }

    public static ProcurementReconciliation Create(
        string code,
        string supplierCode,
        string supplierName,
        DateTime startDate,
        DateTime endDate,
        string description = null)
    {
        if (startDate >= endDate)
            throw new InvalidOperationException("对账开始日期必须早于结束日期");

        return new ProcurementReconciliation
        {
            Code = code,
            SupplierCode = supplierCode,
            SupplierName = supplierName,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            Status = ReconciliationStatus.Draft,
            TotalAmount = 0,
            ConfirmedAmount = 0,
            DifferenceAmount = 0
        };
    }

    public void AddItem(
        string orderCode,
        DateTime orderDate,
        decimal orderAmount,
        decimal confirmedAmount,
        string remark = null)
    {
        if (Status != ReconciliationStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的对账单可以添加项目");

        if (orderAmount < 0)
            throw new InvalidOperationException("订单金额不能为负数");

        if (confirmedAmount < 0)
            throw new InvalidOperationException("确认金额不能为负数");

        var item = new ReconciliationItem(
            orderCode,
            orderDate,
            orderAmount,
            confirmedAmount,
            remark);

        _items.Add(item);
        CalculateAmount();
    }

    public void RemoveItem(string orderCode)
    {
        if (Status != ReconciliationStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的对账单可以删除项目");

        var item = _items.Find(x => x.OrderCode == orderCode);
        if (item != null)
        {
            _items.Remove(item);
            CalculateAmount();
        }
    }

    private void CalculateAmount()
    {
        TotalAmount = 0;
        ConfirmedAmount = 0;
        foreach (var item in _items)
        {
            TotalAmount += item.OrderAmount;
            ConfirmedAmount += item.ConfirmedAmount;
        }
        DifferenceAmount = TotalAmount - ConfirmedAmount;
    }

    public void Submit()
    {
        if (Status != ReconciliationStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的对账单可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("对账单必须包含至少一个项目");

        Status = ReconciliationStatus.Submitted;
    }

    public void Confirm()
    {
        if (Status != ReconciliationStatus.Submitted)
            throw new InvalidOperationException("只有已提交的对账单可以确认");

        Status = ReconciliationStatus.Confirmed;
    }

    public void Reject(string reason)
    {
        if (Status != ReconciliationStatus.Submitted)
            throw new InvalidOperationException("只有已提交的对账单可以驳回");

        Status = ReconciliationStatus.Rejected;
    }

    public void Complete()
    {
        if (Status != ReconciliationStatus.Confirmed)
            throw new InvalidOperationException("只有已确认的对账单可以完成");

        Status = ReconciliationStatus.Completed;
    }
}

public class ReconciliationItem : Entity
{
    public string OrderCode { get; private set; }
    public DateTime OrderDate { get; private set; }
    public decimal OrderAmount { get; private set; }
    public decimal ConfirmedAmount { get; private set; }
    public decimal DifferenceAmount { get; private set; }
    public string Remark { get; private set; }

    private ReconciliationItem() { }

    public ReconciliationItem(
        string orderCode,
        DateTime orderDate,
        decimal orderAmount,
        decimal confirmedAmount,
        string remark = null)
    {
        OrderCode = orderCode;
        OrderDate = orderDate;
        OrderAmount = orderAmount;
        ConfirmedAmount = confirmedAmount;
        DifferenceAmount = orderAmount - confirmedAmount;
        Remark = remark;
    }
}

public enum ReconciliationStatus
{
    Draft = 1,      // 草稿
    Submitted = 2,  // 已提交
    Confirmed = 3,  // 已确认
    Rejected = 4,   // 已驳回
    Completed = 5   // 已完成
}