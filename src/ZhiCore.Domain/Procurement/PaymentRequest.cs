using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Finance;

namespace ZhiCore.Domain.Procurement;

/// <summary>
/// 付款申请
/// </summary>
public class PaymentRequest : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Title { get; private set; }
    public string SupplierCode { get; private set; }
    public string SupplierName { get; private set; }
    public decimal Amount { get; private set; }
    public string AccountCode { get; private set; }
    public PaymentRequestStatus Status { get; private set; }
    public string Description { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    private readonly List<PaymentRequestItem> _items = new();
    public IReadOnlyCollection<PaymentRequestItem> Items => _items.AsReadOnly();

    private PaymentRequest() { }

    public static PaymentRequest Create(
        string code,
        string title,
        string supplierCode,
        string supplierName,
        string accountCode,
        string description = null)
    {
        return new PaymentRequest
        {
            Code = code,
            Title = title,
            SupplierCode = supplierCode,
            SupplierName = supplierName,
            AccountCode = accountCode,
            Description = description,
            Status = PaymentRequestStatus.Draft,
            Amount = 0
        };
    }

    public void AddItem(string purchaseOrderCode, decimal amount, string comment = null)
    {
        if (Status != PaymentRequestStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的付款申请才能添加付款项目");

        if (_items.Any(i => i.PurchaseOrderCode == purchaseOrderCode))
            throw new InvalidOperationException("该采购订单已添加付款项目");

        var item = new PaymentRequestItem(purchaseOrderCode, amount, comment);
        _items.Add(item);
        Amount += amount;
    }

    public void Submit()
    {
        if (Status != PaymentRequestStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的付款申请才能提交");

        if (!_items.Any())
            throw new InvalidOperationException("付款申请必须包含至少一个付款项目");

        Status = PaymentRequestStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != PaymentRequestStatus.Submitted)
            throw new InvalidOperationException("只有已提交的付款申请才能审批");

        Status = PaymentRequestStatus.Approved;
    }

    public void Reject(string reason)
    {
        if (Status != PaymentRequestStatus.Submitted)
            throw new InvalidOperationException("只有已提交的付款申请才能驳回");

        Status = PaymentRequestStatus.Rejected;
        Description = reason;
    }

    public void Pay(DateTime paymentDate)
    {
        if (Status != PaymentRequestStatus.Approved)
            throw new InvalidOperationException("只有已审批的付款申请才能支付");

        PaymentDate = paymentDate;
        Status = PaymentRequestStatus.Paid;
    }

    public void Cancel()
    {
        if (Status == PaymentRequestStatus.Cancelled || Status == PaymentRequestStatus.Paid)
            throw new InvalidOperationException("已支付或取消的付款申请不能取消");

        Status = PaymentRequestStatus.Cancelled;
    }
}

public class PaymentRequestItem : Entity
{
    public string PurchaseOrderCode { get; private set; }
    public decimal Amount { get; private set; }
    public string Comment { get; private set; }

    public PaymentRequestItem(string purchaseOrderCode, decimal amount, string comment = null)
    {
        PurchaseOrderCode = purchaseOrderCode;
        Amount = amount;
        Comment = comment;
    }
}

public enum PaymentRequestStatus
{
    Draft = 1,
    Submitted = 2,
    Approved = 3,
    Rejected = 4,
    Paid = 5,
    Cancelled = 6
}