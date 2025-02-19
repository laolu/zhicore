using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Finance;

namespace ZhiCore.Domain.Procurement;

/// <summary>
/// 采购发票
/// </summary>
public class Invoice : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string InvoiceNumber { get; private set; }
    public string SupplierCode { get; private set; }
    public string SupplierName { get; private set; }
    public decimal Amount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime InvoiceDate { get; private set; }
    public InvoiceStatus Status { get; private set; }
    public string Description { get; private set; }
    private readonly List<InvoiceItem> _items = new();
    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

    private Invoice() { }

    public static Invoice Create(
        string code,
        string invoiceNumber,
        string supplierCode,
        string supplierName,
        DateTime invoiceDate,
        decimal amount,
        decimal taxAmount,
        string description = null)
    {
        return new Invoice
        {
            Code = code,
            InvoiceNumber = invoiceNumber,
            SupplierCode = supplierCode,
            SupplierName = supplierName,
            InvoiceDate = invoiceDate,
            Amount = amount,
            TaxAmount = taxAmount,
            TotalAmount = amount + taxAmount,
            Description = description,
            Status = InvoiceStatus.Draft
        };
    }

    public void AddItem(string purchaseOrderCode, decimal amount, decimal taxAmount, string comment = null)
    {
        if (Status != InvoiceStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的发票才能添加明细");

        if (_items.Any(i => i.PurchaseOrderCode == purchaseOrderCode))
            throw new InvalidOperationException("该采购订单已添加发票明细");

        var item = new InvoiceItem(purchaseOrderCode, amount, taxAmount, comment);
        _items.Add(item);
    }

    public void Submit()
    {
        if (Status != InvoiceStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的发票才能提交");

        if (!_items.Any())
            throw new InvalidOperationException("发票必须包含至少一个明细项目");

        if (_items.Sum(i => i.Amount + i.TaxAmount) != TotalAmount)
            throw new InvalidOperationException("发票明细金额与发票总金额不符");

        Status = InvoiceStatus.Submitted;
    }

    public void Verify()
    {
        if (Status != InvoiceStatus.Submitted)
            throw new InvalidOperationException("只有已提交的发票才能验证");

        Status = InvoiceStatus.Verified;
    }

    public void Reject(string reason)
    {
        if (Status != InvoiceStatus.Submitted)
            throw new InvalidOperationException("只有已提交的发票才能驳回");

        Status = InvoiceStatus.Rejected;
        Description = reason;
    }

    public void WriteOff()
    {
        if (Status != InvoiceStatus.Verified)
            throw new InvalidOperationException("只有已验证的发票才能核销");

        Status = InvoiceStatus.WrittenOff;
    }

    public void Cancel()
    {
        if (Status == InvoiceStatus.Cancelled || Status == InvoiceStatus.WrittenOff)
            throw new InvalidOperationException("已核销或取消的发票不能取消");

        Status = InvoiceStatus.Cancelled;
    }
}

public class InvoiceItem : Entity
{
    public string PurchaseOrderCode { get; private set; }
    public decimal Amount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public string Comment { get; private set; }

    public InvoiceItem(string purchaseOrderCode, decimal amount, decimal taxAmount, string comment = null)
    {
        PurchaseOrderCode = purchaseOrderCode;
        Amount = amount;
        TaxAmount = taxAmount;
        Comment = comment;
    }
}

public enum InvoiceStatus
{
    Draft = 1,
    Submitted = 2,
    Verified = 3,
    Rejected = 4,
    WrittenOff = 5,
    Cancelled = 6
}