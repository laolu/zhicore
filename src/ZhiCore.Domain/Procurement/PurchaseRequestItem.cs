using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class PurchaseRequestItem : AuditableEntity
{
    public int ProductId { get; private set; }
    public string Specification { get; private set; }
    public int Quantity { get; private set; }
    public decimal EstimatedUnitPrice { get; private set; }
    public decimal EstimatedTotalPrice => EstimatedUnitPrice * Quantity;

    private PurchaseRequestItem() { }

    public PurchaseRequestItem(
        int productId,
        string specification,
        int quantity,
        decimal estimatedUnitPrice)
    {
        if (productId <= 0)
            throw new ArgumentException("商品ID必须大于0", nameof(productId));

        if (string.IsNullOrWhiteSpace(specification))
            throw new ArgumentException("规格说明不能为空", nameof(specification));

        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));

        if (estimatedUnitPrice <= 0)
            throw new ArgumentException("预估单价必须大于0", nameof(estimatedUnitPrice));

        ProductId = productId;
        Specification = specification;
        Quantity = quantity;
        EstimatedUnitPrice = estimatedUnitPrice;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(newQuantity));

        Quantity = newQuantity;
    }

    public void UpdateEstimatedUnitPrice(decimal newEstimatedUnitPrice)
    {
        if (newEstimatedUnitPrice <= 0)
            throw new ArgumentException("预估单价必须大于0", nameof(newEstimatedUnitPrice));

        EstimatedUnitPrice = newEstimatedUnitPrice;
    }

    public void UpdateSpecification(string newSpecification)
    {
        if (string.IsNullOrWhiteSpace(newSpecification))
            throw new ArgumentException("规格说明不能为空", nameof(newSpecification));

        Specification = newSpecification;
    }
}