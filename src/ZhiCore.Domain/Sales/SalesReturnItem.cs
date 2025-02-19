using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class SalesReturnItem : Entity
{
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalAmount => UnitPrice * Quantity;
    public string Remarks { get; private set; }

    private SalesReturnItem() { }

    public SalesReturnItem(
        int productId,
        int quantity,
        decimal unitPrice,
        string remarks = null)
    {
        if (productId <= 0)
            throw new ArgumentException("商品ID必须大于0", nameof(productId));

        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));

        if (unitPrice < 0)
            throw new ArgumentException("单价不能为负数", nameof(unitPrice));

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Remarks = remarks;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(newQuantity));

        Quantity = newQuantity;
    }

    public void UpdateRemarks(string newRemarks)
    {
        Remarks = newRemarks;
    }
}