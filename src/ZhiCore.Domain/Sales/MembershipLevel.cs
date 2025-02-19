using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class MembershipLevel : Entity
{
    public string Name { get; private set; }
    public int RequiredPoints { get; private set; }
    public decimal DiscountRate { get; private set; }
    public List<MemberBenefit> Benefits { get; private set; }

    public MembershipLevel(string name, int requiredPoints, decimal discountRate)
    {
        Name = name;
        RequiredPoints = requiredPoints;
        DiscountRate = discountRate;
        Benefits = new List<MemberBenefit>();
    }

    public void AddBenefit(string description, string type)
    {
        var benefit = new MemberBenefit(description, type);
        Benefits.Add(benefit);
    }

    public void UpdateDiscountRate(decimal newRate)
    {
        if (newRate < 0 || newRate > 1)
            throw new ArgumentException("折扣率必须在0到1之间");

        DiscountRate = newRate;
    }
}

public class MemberBenefit
{
    public string Description { get; private set; }
    public string Type { get; private set; }

    public MemberBenefit(string description, string type)
    {
        Description = description;
        Type = type;
    }
}