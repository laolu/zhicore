using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Sales.Events;

namespace ZhiCore.Domain.Sales;

public class SalesReturn : AuditableEntity
{
    public string ReturnNumber { get; private set; }
    public string SalesOrderNumber { get; private set; }
    public int CustomerId { get; private set; }
    public DateTime ReturnDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public SalesReturnStatus Status { get; private set; }
    public string Reason { get; private set; }
    private readonly List<SalesReturnItem> _items = new();
    public IReadOnlyCollection<SalesReturnItem> Items => _items.AsReadOnly();

    private SalesReturn() { }

    public static SalesReturn Create(
        string salesOrderNumber,
        int customerId,
        DateTime returnDate,
        string reason)
    {
        if (string.IsNullOrWhiteSpace(salesOrderNumber))
            throw new ArgumentException("销售订单号不能为空", nameof(salesOrderNumber));

        if (customerId <= 0)
            throw new ArgumentException("客户ID必须大于0", nameof(customerId));

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("退货原因不能为空", nameof(reason));

        return new SalesReturn
        {
            ReturnNumber = GenerateReturnNumber(),
            SalesOrderNumber = salesOrderNumber,
            CustomerId = customerId,
            ReturnDate = returnDate,
            Status = SalesReturnStatus.Draft,
            Reason = reason
        };
    }

    public void AddItem(int productId, int quantity, decimal unitPrice, string remarks = null)
    {
        if (Status != SalesReturnStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的退货单可以修改明细");

        var item = new SalesReturnItem(productId, quantity, unitPrice, remarks);
        _items.Add(item);
        UpdateTotalAmount();
    }

    public void Submit()
    {
        if (Status != SalesReturnStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的退货单可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("退货单必须包含至少一个商品");

        Status = SalesReturnStatus.Submitted;
        AddDomainEvent(new SalesReturnStatusChangedEvent(ReturnNumber, SalesReturnStatus.Draft, Status));
    }

    public void Approve()
    {
        if (Status != SalesReturnStatus.Submitted)
            throw new InvalidOperationException("只有已提交的退货单可以审批");

        Status = SalesReturnStatus.Approved;
        AddDomainEvent(new SalesReturnStatusChangedEvent(ReturnNumber, SalesReturnStatus.Submitted, Status));
    }

    public void Reject(string rejectReason)
    {
        if (Status != SalesReturnStatus.Submitted)
            throw new InvalidOperationException("只有已提交的退货单可以驳回");

        Status = SalesReturnStatus.Rejected;
        Reason += $"\n驳回原因：{rejectReason}";
        AddDomainEvent(new SalesReturnStatusChangedEvent(ReturnNumber, SalesReturnStatus.Submitted, Status));
    }

    public void CompleteReturn()
    {
        if (Status != SalesReturnStatus.Approved)
            throw new InvalidOperationException("只有已审批的退货单可以完成退货");

        Status = SalesReturnStatus.Completed;
        AddDomainEvent(new SalesReturnStatusChangedEvent(ReturnNumber, SalesReturnStatus.Approved, Status));
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalAmount);
    }

    private static string GenerateReturnNumber()
    {
        return $"SR{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum SalesReturnStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3,
    Completed = 4
}