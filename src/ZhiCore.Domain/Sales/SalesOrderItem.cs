using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class SalesOrderItem : AuditableEntity
{
    public int ProductId { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    private SalesOrderItem() { }

    public SalesOrderItem(int productId, decimal unitPrice, int quantity)
    {
        if (productId <= 0)
            throw new ArgumentException("商品ID必须大于0", nameof(productId));

        if (unitPrice <= 0)
            throw new ArgumentException("单价必须大于0", nameof(unitPrice));

        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));

        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(newQuantity));

        Quantity = newQuantity;
    }

    public void UpdateUnitPrice(decimal newUnitPrice)
    {
        if (newUnitPrice <= 0)
            throw new ArgumentException("单价必须大于0", nameof(newUnitPrice));

        UnitPrice = newUnitPrice;
    }
}