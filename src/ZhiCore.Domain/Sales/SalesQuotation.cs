using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Sales.Events;

namespace ZhiCore.Domain.Sales;

public class SalesQuotation : AuditableEntity
{
    public string QuotationNumber { get; private set; }
    public int CustomerId { get; private set; }
    public DateTime QuotationDate { get; private set; }
    public DateTime ValidUntil { get; private set; }
    public decimal TotalAmount { get; private set; }
    public SalesQuotationStatus Status { get; private set; }
    public string Remarks { get; private set; }
    private readonly List<SalesQuotationItem> _items = new();
    public IReadOnlyCollection<SalesQuotationItem> Items => _items.AsReadOnly();

    private SalesQuotation() { }

    public static SalesQuotation Create(
        int customerId,
        DateTime quotationDate,
        DateTime validUntil,
        string remarks = null)
    {
        if (customerId <= 0)
            throw new ArgumentException("客户ID必须大于0", nameof(customerId));

        if (validUntil <= quotationDate)
            throw new ArgumentException("有效期必须大于报价日期", nameof(validUntil));

        return new SalesQuotation
        {
            QuotationNumber = GenerateQuotationNumber(),
            CustomerId = customerId,
            QuotationDate = quotationDate,
            ValidUntil = validUntil,
            Status = SalesQuotationStatus.Draft,
            Remarks = remarks
        };
    }

    public void AddItem(int productId, decimal unitPrice, int quantity, string remarks = null)
    {
        if (Status != SalesQuotationStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的报价单可以修改明细");

        var item = new SalesQuotationItem(productId, unitPrice, quantity, remarks);
        _items.Add(item);
        UpdateTotalAmount();
    }

    public void Submit()
    {
        if (Status != SalesQuotationStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的报价单可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("报价单必须包含至少一个商品");

        Status = SalesQuotationStatus.Submitted;
        AddDomainEvent(new SalesQuotationStatusChangedEvent(QuotationNumber, SalesQuotationStatus.Draft, Status));
    }

    public void Approve()
    {
        if (Status != SalesQuotationStatus.Submitted)
            throw new InvalidOperationException("只有已提交的报价单可以审批");

        Status = SalesQuotationStatus.Approved;
        AddDomainEvent(new SalesQuotationStatusChangedEvent(QuotationNumber, SalesQuotationStatus.Submitted, Status));
    }

    public void Reject(string reason)
    {
        if (Status != SalesQuotationStatus.Submitted)
            throw new InvalidOperationException("只有已提交的报价单可以驳回");

        Status = SalesQuotationStatus.Rejected;
        Remarks = reason;
        AddDomainEvent(new SalesQuotationStatusChangedEvent(QuotationNumber, SalesQuotationStatus.Submitted, Status));
    }

    public SalesOrder ConvertToOrder()
    {
        if (Status != SalesQuotationStatus.Approved)
            throw new InvalidOperationException("只有已审批的报价单可以转换为销售订单");

        if (ValidUntil < DateTime.Now)
            throw new InvalidOperationException("报价单已过期");

        var order = SalesOrder.Create(CustomerId, DateTime.Now, Remarks);
        foreach (var item in _items)
        {
            order.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
        }

        Status = SalesQuotationStatus.Converted;
        AddDomainEvent(new SalesQuotationStatusChangedEvent(QuotationNumber, SalesQuotationStatus.Approved, Status));

        return order;
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalPrice);
    }

    private static string GenerateQuotationNumber()
    {
        return $"SQ{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum SalesQuotationStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3,
    Converted = 4,
    Expired = 5
}