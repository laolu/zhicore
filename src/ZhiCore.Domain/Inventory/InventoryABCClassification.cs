using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryABCClassification : AuditableEntity
{
    public int ProductId { get; private set; }
    public ClassificationType Classification { get; private set; }
    public decimal AnnualUsageValue { get; private set; }
    public int AnnualUsageCount { get; private set; }
    public decimal PercentageOfTotal { get; private set; }
    public DateTime LastAnalysisDate { get; private set; }

    private InventoryABCClassification() { }

    public static InventoryABCClassification Create(
        int productId,
        decimal annualUsageValue,
        int annualUsageCount,
        decimal percentageOfTotal)
    {
        if (productId <= 0)
            throw new ArgumentException("商品ID必须大于0", nameof(productId));

        if (annualUsageValue < 0)
            throw new ArgumentException("年度使用价值不能为负数", nameof(annualUsageValue));

        if (annualUsageCount < 0)
            throw new ArgumentException("年度使用次数不能为负数", nameof(annualUsageCount));

        if (percentageOfTotal < 0 || percentageOfTotal > 100)
            throw new ArgumentException("总价值百分比必须在0-100之间", nameof(percentageOfTotal));

        var classification = DetermineClassification(percentageOfTotal);

        return new InventoryABCClassification
        {
            ProductId = productId,
            Classification = classification,
            AnnualUsageValue = annualUsageValue,
            AnnualUsageCount = annualUsageCount,
            PercentageOfTotal = percentageOfTotal,
            LastAnalysisDate = DateTime.Now
        };
    }

    public void UpdateClassification(
        decimal annualUsageValue,
        int annualUsageCount,
        decimal percentageOfTotal)
    {
        if (annualUsageValue < 0)
            throw new ArgumentException("年度使用价值不能为负数", nameof(annualUsageValue));

        if (annualUsageCount < 0)
            throw new ArgumentException("年度使用次数不能为负数", nameof(annualUsageCount));

        if (percentageOfTotal < 0 || percentageOfTotal > 100)
            throw new ArgumentException("总价值百分比必须在0-100之间", nameof(percentageOfTotal));

        AnnualUsageValue = annualUsageValue;
        AnnualUsageCount = annualUsageCount;
        PercentageOfTotal = percentageOfTotal;
        Classification = DetermineClassification(percentageOfTotal);
        LastAnalysisDate = DateTime.Now;
    }

    private static ClassificationType DetermineClassification(decimal percentageOfTotal)
    {
        return percentageOfTotal switch
        {
            <= 80 => ClassificationType.A,
            <= 95 => ClassificationType.B,
            _ => ClassificationType.C
        };
    }
}

public enum ClassificationType
{
    A = 1, // 高价值物品（约占总价值的80%）
    B = 2, // 中价值物品（约占总价值的15%）
    C = 3  // 低价值物品（约占总价值的5%）
}