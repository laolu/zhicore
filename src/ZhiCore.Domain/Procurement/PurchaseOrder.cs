using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class PurchaseOrder : AuditableEntity
{
    public string OrderNumber { get; private set; }
    public int SupplierId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public PurchaseOrderStatus Status { get; private set; }
    public string Remarks { get; private set; }
    private readonly List<PurchaseOrderItem> _items = new();
    public IReadOnlyCollection<PurchaseOrderItem> Items => _items.AsReadOnly();

    private PurchaseOrder() { }

    public static PurchaseOrder Create(int supplierId, DateTime orderDate, string remarks = null)
    {
        return new PurchaseOrder
        {
            OrderNumber = GenerateOrderNumber(),
            SupplierId = supplierId,
            OrderDate = orderDate,
            Status = PurchaseOrderStatus.Draft,
            Remarks = remarks
        };
    }

    public void AddItem(int productId, decimal unitPrice, int quantity)
    {
        var item = new PurchaseOrderItem(productId, unitPrice, quantity);
        _items.Add(item);
        UpdateTotalAmount();
    }

    public void Submit()
    {
        if (Status != PurchaseOrderStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的采购订单可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("采购订单必须包含至少一个商品");

        var oldStatus = Status;
        Status = PurchaseOrderStatus.Submitted;
        AddDomainEvent(new PurchaseOrderStatusChangedEvent(OrderNumber, oldStatus, Status));
    }

    public void Approve()
    {
        if (Status != PurchaseOrderStatus.Submitted)
            throw new InvalidOperationException("只有已提交的采购订单可以审批");

        var oldStatus = Status;
        Status = PurchaseOrderStatus.Approved;
        AddDomainEvent(new PurchaseOrderStatusChangedEvent(OrderNumber, oldStatus, Status));
    }
    private void UpdateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalPrice);
    }

    private static string GenerateOrderNumber()
    {
        return $"PO{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum PurchaseOrderStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3,
    Completed = 4,
    Cancelled = 5
}