using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class PointsRule : Entity
{
    public string Name { get; private set; }
    public string Type { get; private set; } // 例如：消费积分、活动积分、推荐积分等
    public decimal PointsPerAmount { get; private set; } // 每消费金额获得的积分
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public bool IsActive { get; private set; }

    public PointsRule(string name, string type, decimal pointsPerAmount, DateTime effectiveDate, DateTime? expiryDate = null)
    {
        if (pointsPerAmount < 0)
            throw new ArgumentException("每消费金额获得的积分不能为负数");

        Name = name;
        Type = type;
        PointsPerAmount = pointsPerAmount;
        EffectiveDate = effectiveDate;
        ExpiryDate = expiryDate;
        IsActive = true;
    }

    public void UpdatePointsRate(decimal newPointsPerAmount)
    {
        if (newPointsPerAmount < 0)
            throw new ArgumentException("每消费金额获得的积分不能为负数");

        PointsPerAmount = newPointsPerAmount;
    }

    public void Deactivate()
    {
        IsActive = false;
        ExpiryDate = DateTime.Now;
    }

    public void Activate()
    {
        if (ExpiryDate.HasValue && ExpiryDate.Value <= DateTime.Now)
            throw new InvalidOperationException("已过期的积分规则不能被激活");

        IsActive = true;
    }
}