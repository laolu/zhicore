using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

/// <summary>
/// 询价单
/// </summary>
public class RFQ : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Title { get; private set; }
    public DateTime ValidUntil { get; private set; }
    public RFQStatus Status { get; private set; }
    public string Description { get; private set; }
    private readonly List<RFQItem> _items = new();
    public IReadOnlyCollection<RFQItem> Items => _items.AsReadOnly();
    private readonly List<RFQQuotation> _quotations = new();
    public IReadOnlyCollection<RFQQuotation> Quotations => _quotations.AsReadOnly();

    private RFQ() { }

    public static RFQ Create(
        string code,
        string title,
        DateTime validUntil,
        string description = null)
    {
        return new RFQ
        {
            Code = code,
            Title = title,
            ValidUntil = validUntil,
            Description = description,
            Status = RFQStatus.Draft
        };
    }

    public void AddItem(string itemCode, string itemName, decimal quantity, string unit, string specification = null)
    {
        if (Status != RFQStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的询价单才能添加物料");

        var item = new RFQItem(itemCode, itemName, quantity, unit, specification);
        _items.Add(item);
    }

    public void Submit()
    {
        if (Status != RFQStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的询价单才能提交");

        if (!_items.Any())
            throw new InvalidOperationException("询价单必须包含至少一个物料");

        Status = RFQStatus.InProgress;
    }

    public void AddQuotation(string supplierCode, string supplierName)
    {
        if (Status != RFQStatus.InProgress)
            throw new InvalidOperationException("只有进行中的询价单才能添加报价");

        if (_quotations.Any(q => q.SupplierCode == supplierCode))
            throw new InvalidOperationException("该供应商已提交报价");

        var quotation = new RFQQuotation(supplierCode, supplierName);
        _quotations.Add(quotation);
    }

    public void Close()
    {
        if (Status != RFQStatus.InProgress)
            throw new InvalidOperationException("只有进行中的询价单才能关闭");

        Status = RFQStatus.Closed;
    }

    public void Cancel()
    {
        if (Status == RFQStatus.Cancelled || Status == RFQStatus.Closed)
            throw new InvalidOperationException("已关闭或取消的询价单不能取消");

        Status = RFQStatus.Cancelled;
    }
}

public class RFQItem : Entity
{
    public string ItemCode { get; private set; }
    public string ItemName { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }
    public string Specification { get; private set; }

    public RFQItem(string itemCode, string itemName, decimal quantity, string unit, string specification = null)
    {
        ItemCode = itemCode;
        ItemName = itemName;
        Quantity = quantity;
        Unit = unit;
        Specification = specification;
    }
}

public class RFQQuotation : Entity
{
    public string SupplierCode { get; private set; }
    public string SupplierName { get; private set; }
    public DateTime QuotedAt { get; private set; }
    private readonly List<RFQQuotationItem> _items = new();
    public IReadOnlyCollection<RFQQuotationItem> Items => _items.AsReadOnly();

    public RFQQuotation(string supplierCode, string supplierName)
    {
        SupplierCode = supplierCode;
        SupplierName = supplierName;
        QuotedAt = DateTime.Now;
    }

    public void AddItem(string itemCode, decimal unitPrice, string comment = null)
    {
        if (_items.Any(i => i.ItemCode == itemCode))
            throw new InvalidOperationException("该物料已报价");

        var item = new RFQQuotationItem(itemCode, unitPrice, comment);
        _items.Add(item);
    }
}

public class RFQQuotationItem : Entity
{
    public string ItemCode { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string Comment { get; private set; }

    public RFQQuotationItem(string itemCode, decimal unitPrice, string comment = null)
    {
        ItemCode = itemCode;
        UnitPrice = unitPrice;
        Comment = comment;
    }
}

public enum RFQStatus
{
    Draft = 1,
    InProgress = 2,
    Closed = 3,
    Cancelled = 4
}