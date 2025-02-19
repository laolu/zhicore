using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class Promotion : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsActive { get; private set; }
    public PromotionType Type { get; private set; }
    public List<PromotionRule> Rules { get; private set; }
    public List<PromotionReward> Rewards { get; private set; }

    public Promotion(string name, string description, DateTime startTime, DateTime endTime, PromotionType type)
    {
        if (startTime >= endTime)
            throw new ArgumentException("活动开始时间必须早于结束时间");

        Name = name;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        Type = type;
        IsActive = true;
        Rules = new List<PromotionRule>();
        Rewards = new List<PromotionReward>();
    }

    public void AddRule(string condition, string value)
    {
        var rule = new PromotionRule(condition, value);
        Rules.Add(rule);
    }

    public void AddReward(string type, string value, decimal amount)
    {
        var reward = new PromotionReward(type, value, amount);
        Rewards.Add(reward);
    }

    public void Deactivate()
    {
        IsActive = false;
        EndTime = DateTime.Now;
    }

    public bool IsValidNow()
    {
        var now = DateTime.Now;
        return IsActive && now >= StartTime && now <= EndTime;
    }
}

public enum PromotionType
{
    Discount, // 折扣
    CashBack, // 返现
    PointsMultiplier, // 积分倍增
    GiftWithPurchase, // 赠品
    BundleDeal, // 套餐优惠
    FlashSale // 限时特价
}

public class PromotionRule
{
    public string Condition { get; private set; } // 例如：最低消费金额、指定商品类别等
    public string Value { get; private set; }

    public PromotionRule(string condition, string value)
    {
        Condition = condition;
        Value = value;
    }
}

public class PromotionReward
{
    public string Type { get; private set; } // 例如：折扣、返现、积分等
    public string Value { get; private set; }
    public decimal Amount { get; private set; }

    public PromotionReward(string type, string value, decimal amount)
    {
        Type = type;
        Value = value;
        Amount = amount;
    }
}