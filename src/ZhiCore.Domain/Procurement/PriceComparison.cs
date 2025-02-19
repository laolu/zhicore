using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

/// <summary>
/// 比价单
/// </summary>
public class PriceComparison : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Title { get; private set; }
    public PriceComparisonStatus Status { get; private set; }
    public string Description { get; private set; }
    public string WinningSupplierCode { get; private set; }
    public string WinningSupplierName { get; private set; }
    private readonly List<PriceComparisonItem> _items = new();
    public IReadOnlyCollection<PriceComparisonItem> Items => _items.AsReadOnly();
    private readonly List<PriceComparisonQuotation> _quotations = new();
    public IReadOnlyCollection<PriceComparisonQuotation> Quotations => _quotations.AsReadOnly();

    private PriceComparison() { }

    public static PriceComparison Create(
        string code,
        string title,
        string description = null)
    {
        return new PriceComparison
        {
            Code = code,
            Title = title,
            Description = description,
            Status = PriceComparisonStatus.Draft
        };
    }

    public void AddItem(string itemCode, string itemName, decimal quantity, string unit, string specification = null)
    {
        if (Status != PriceComparisonStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的比价单才能添加物料");

        var item = new PriceComparisonItem(itemCode, itemName, quantity, unit, specification);
        _items.Add(item);
    }

    public void AddQuotation(string supplierCode, string supplierName)
    {
        if (Status != PriceComparisonStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的比价单才能添加报价");

        if (_quotations.Any(q => q.SupplierCode == supplierCode))
            throw new InvalidOperationException("该供应商已提交报价");

        var quotation = new PriceComparisonQuotation(supplierCode, supplierName);
        _quotations.Add(quotation);
    }

    public void Submit()
    {
        if (Status != PriceComparisonStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的比价单才能提交");

        if (!_items.Any())
            throw new InvalidOperationException("比价单必须包含至少一个物料");

        if (_quotations.Count < 2)
            throw new InvalidOperationException("比价单必须包含至少两个供应商报价");

        Status = PriceComparisonStatus.InProgress;
    }

    public void SelectWinner(string supplierCode)
    {
        if (Status != PriceComparisonStatus.InProgress)
            throw new InvalidOperationException("只有进行中的比价单才能选择中标供应商");

        var winner = _quotations.FirstOrDefault(q => q.SupplierCode == supplierCode);
        if (winner == null)
            throw new InvalidOperationException("未找到指定的供应商报价");

        WinningSupplierCode = winner.SupplierCode;
        WinningSupplierName = winner.SupplierName;
        Status = PriceComparisonStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == PriceComparisonStatus.Cancelled || Status == PriceComparisonStatus.Completed)
            throw new InvalidOperationException("已完成或取消的比价单不能取消");

        Status = PriceComparisonStatus.Cancelled;
    }
}

public class PriceComparisonItem : Entity
{
    public string ItemCode { get; private set; }
    public string ItemName { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }
    public string Specification { get; private set; }

    public PriceComparisonItem(string itemCode, string itemName, decimal quantity, string unit, string specification = null)
    {
        ItemCode = itemCode;
        ItemName = itemName;
        Quantity = quantity;
        Unit = unit;
        Specification = specification;
    }
}

public class PriceComparisonQuotation : Entity
{
    public string SupplierCode { get; private set; }
    public string SupplierName { get; private set; }
    public DateTime QuotedAt { get; private set; }
    private readonly List<PriceComparisonQuotationItem> _items = new();
    public IReadOnlyCollection<PriceComparisonQuotationItem> Items => _items.AsReadOnly();

    public PriceComparisonQuotation(string supplierCode, string supplierName)
    {
        SupplierCode = supplierCode;
        SupplierName = supplierName;
        QuotedAt = DateTime.Now;
    }

    public void AddItem(string itemCode, decimal unitPrice, string comment = null)
    {
        if (_items.Any(i => i.ItemCode == itemCode))
            throw new InvalidOperationException("该物料已报价");

        var item = new PriceComparisonQuotationItem(itemCode, unitPrice, comment);
        _items.Add(item);
    }

    public decimal GetTotalAmount()
    {
        return _items.Sum(i => i.UnitPrice);
    }
}

public class PriceComparisonQuotationItem : Entity
{
    public string ItemCode { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string Comment { get; private set; }

    public PriceComparisonQuotationItem(string itemCode, decimal unitPrice, string comment = null)
    {
        ItemCode = itemCode;
        UnitPrice = unitPrice;
        Comment = comment;
    }
}

public enum PriceComparisonStatus
{
    Draft = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}