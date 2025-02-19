using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class SalesOrder : AuditableEntity
{
    public string OrderNumber { get; private set; }
    public int CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal FinalAmount => TotalAmount - DiscountAmount;
    public SalesOrderStatus Status { get; private set; }
    public string Remarks { get; private set; }
    private readonly List<SalesOrderItem> _items = new();
    public IReadOnlyCollection<SalesOrderItem> Items => _items.AsReadOnly();

    private SalesOrder() { }

    public static SalesOrder Create(int customerId, DateTime orderDate, string remarks = null)
    {
        if (customerId <= 0)
            throw new ArgumentException("客户ID必须大于0", nameof(customerId));

        return new SalesOrder
        {
            OrderNumber = GenerateOrderNumber(),
            CustomerId = customerId,
            OrderDate = orderDate,
            Status = SalesOrderStatus.Draft,
            Remarks = remarks,
            DiscountAmount = 0
        };
    }

    public void AddItem(int productId, decimal unitPrice, int quantity)
    {
        var item = new SalesOrderItem(productId, unitPrice, quantity);
        _items.Add(item);
        UpdateTotalAmount();
    }

    public void ApplyDiscount(decimal discountAmount)
    {
        if (discountAmount < 0)
            throw new ArgumentException("折扣金额不能为负数", nameof(discountAmount));

        if (discountAmount > TotalAmount)
            throw new ArgumentException("折扣金额不能大于订单总金额", nameof(discountAmount));

        DiscountAmount = discountAmount;
    }

    public void Submit()
    {
        if (Status != SalesOrderStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的销售订单可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("销售订单必须包含至少一个商品");

        Status = SalesOrderStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != SalesOrderStatus.Submitted)
            throw new InvalidOperationException("只有已提交的销售订单可以审批");

        Status = SalesOrderStatus.Approved;
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalPrice);
    }

    private static string GenerateOrderNumber()
    {
        return $"SO{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum SalesOrderStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3,
    Completed = 4,
    Cancelled = 5
}