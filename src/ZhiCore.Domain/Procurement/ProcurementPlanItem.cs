using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class ProcurementPlanItem : Entity
{
    public string MaterialCode { get; private set; }
    public string MaterialName { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }
    public decimal EstimatedPrice { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime ExpectedDeliveryDate { get; private set; }
    public string Remark { get; private set; }

    private ProcurementPlanItem() { }

    public ProcurementPlanItem(
        string materialCode,
        string materialName,
        decimal quantity,
        string unit,
        decimal estimatedPrice,
        DateTime expectedDeliveryDate,
        string remark = null)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("采购数量必须大于零");

        if (estimatedPrice < 0)
            throw new InvalidOperationException("预估单价不能为负数");

        MaterialCode = materialCode;
        MaterialName = materialName;
        Quantity = quantity;
        Unit = unit;
        EstimatedPrice = estimatedPrice;
        ExpectedDeliveryDate = expectedDeliveryDate;
        Remark = remark;

        CalculateTotalAmount();
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = Quantity * EstimatedPrice;
    }

    public void UpdateQuantity(decimal newQuantity)
    {
        if (newQuantity <= 0)
            throw new InvalidOperationException("采购数量必须大于零");

        Quantity = newQuantity;
        CalculateTotalAmount();
    }

    public void UpdateEstimatedPrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new InvalidOperationException("预估单价不能为负数");

        EstimatedPrice = newPrice;
        CalculateTotalAmount();
    }

    public void UpdateExpectedDeliveryDate(DateTime newDate)
    {
        ExpectedDeliveryDate = newDate;
    }

    public void UpdateRemark(string newRemark)
    {
        Remark = newRemark;
    }
}