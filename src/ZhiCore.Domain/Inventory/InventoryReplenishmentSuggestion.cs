using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryReplenishmentSuggestion : AuditableEntity
{
    public int ProductId { get; private set; }
    public int SuggestedQuantity { get; private set; }
    public decimal CurrentStock { get; private set; }
    public decimal SafetyStock { get; private set; }
    public decimal ReorderPoint { get; private set; }
    public decimal AverageDailyUsage { get; private set; }
    public int LeadTimeDays { get; private set; }
    public ReplenishmentPriority Priority { get; private set; }
    public DateTime SuggestionDate { get; private set; }
    public string Reason { get; private set; }

    private InventoryReplenishmentSuggestion() { }

    public static InventoryReplenishmentSuggestion Create(
        int productId,
        decimal currentStock,
        decimal safetyStock,
        decimal reorderPoint,
        decimal averageDailyUsage,
        int leadTimeDays)
    {
        if (productId <= 0)
            throw new ArgumentException("商品ID必须大于0", nameof(productId));

        if (currentStock < 0)
            throw new ArgumentException("当前库存不能为负数", nameof(currentStock));

        if (safetyStock < 0)
            throw new ArgumentException("安全库存不能为负数", nameof(safetyStock));

        if (reorderPoint < safetyStock)
            throw new ArgumentException("再订货点不能小于安全库存", nameof(reorderPoint));

        if (averageDailyUsage < 0)
            throw new ArgumentException("日均用量不能为负数", nameof(averageDailyUsage));

        if (leadTimeDays <= 0)
            throw new ArgumentException("补货提前期必须大于0", nameof(leadTimeDays));

        var suggestedQuantity = CalculateSuggestedQuantity(currentStock, reorderPoint, averageDailyUsage, leadTimeDays);
        var priority = DeterminePriority(currentStock, safetyStock, reorderPoint);
        var reason = GenerateReason(currentStock, safetyStock, reorderPoint);

        return new InventoryReplenishmentSuggestion
        {
            ProductId = productId,
            SuggestedQuantity = suggestedQuantity,
            CurrentStock = currentStock,
            SafetyStock = safetyStock,
            ReorderPoint = reorderPoint,
            AverageDailyUsage = averageDailyUsage,
            LeadTimeDays = leadTimeDays,
            Priority = priority,
            SuggestionDate = DateTime.Now,
            Reason = reason
        };
    }

    private static int CalculateSuggestedQuantity(
        decimal currentStock,
        decimal reorderPoint,
        decimal averageDailyUsage,
        int leadTimeDays)
    {
        if (currentStock > reorderPoint)
            return 0;

        var expectedUsageDuringLeadTime = averageDailyUsage * leadTimeDays;
        var suggestedQuantity = (int)Math.Ceiling(expectedUsageDuringLeadTime - currentStock);
        return Math.Max(suggestedQuantity, 0);
    }

    private static ReplenishmentPriority DeterminePriority(
        decimal currentStock,
        decimal safetyStock,
        decimal reorderPoint)
    {
        if (currentStock <= safetyStock)
            return ReplenishmentPriority.High;

        if (currentStock <= reorderPoint)
            return ReplenishmentPriority.Medium;

        return ReplenishmentPriority.Low;
    }

    private static string GenerateReason(
        decimal currentStock,
        decimal safetyStock,
        decimal reorderPoint)
    {
        if (currentStock <= safetyStock)
            return "当前库存低于安全库存水平";

        if (currentStock <= reorderPoint)
            return "当前库存达到再订货点";

        return "库存水平正常";
    }
}

public enum ReplenishmentPriority
{
    Low = 1,    // 库存充足，无需紧急补货
    Medium = 2, // 库存接近再订货点，建议准备补货
    High = 3    // 库存低于安全库存，需要立即补货
}