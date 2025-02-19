using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class SalesPlan : AuditableEntity
{
    public string PlanNumber { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal TargetAmount { get; private set; }
    public PlanStatus Status { get; private set; }
    public string Description { get; private set; }
    private readonly List<SalesPlanItem> _items = new();
    public IReadOnlyCollection<SalesPlanItem> Items => _items.AsReadOnly();

    private SalesPlan() { }

    public static SalesPlan Create(
        DateTime startDate,
        DateTime endDate,
        decimal targetAmount,
        string description = null)
    {
        if (startDate > endDate)
            throw new ArgumentException("开始日期不能晚于结束日期");

        if (targetAmount <= 0)
            throw new ArgumentException("目标金额必须大于0", nameof(targetAmount));

        return new SalesPlan
        {
            PlanNumber = GeneratePlanNumber(),
            StartDate = startDate,
            EndDate = endDate,
            TargetAmount = targetAmount,
            Status = PlanStatus.Draft,
            Description = description
        };
    }

    public void AddItem(int productId, int targetQuantity, decimal targetPrice)
    {
        var item = new SalesPlanItem(productId, targetQuantity, targetPrice);
        _items.Add(item);
    }

    public void Submit()
    {
        if (Status != PlanStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的计划可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("销售计划必须包含至少一个商品");

        Status = PlanStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != PlanStatus.Submitted)
            throw new InvalidOperationException("只有已提交的计划可以审批");

        Status = PlanStatus.Active;
    }

    public void Reject(string reason)
    {
        if (Status != PlanStatus.Submitted)
            throw new InvalidOperationException("只有已提交的计划可以驳回");

        Status = PlanStatus.Rejected;
        Description = reason;
    }

    private static string GeneratePlanNumber()
    {
        return $"SP{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public class SalesPlanItem : AuditableEntity
{
    public int ProductId { get; private set; }
    public int TargetQuantity { get; private set; }
    public decimal TargetPrice { get; private set; }
    public decimal TargetAmount => TargetQuantity * TargetPrice;

    private SalesPlanItem() { }

    public SalesPlanItem(int productId, int targetQuantity, decimal targetPrice)
    {
        if (productId <= 0)
            throw new ArgumentException("商品ID必须大于0", nameof(productId));

        if (targetQuantity <= 0)
            throw new ArgumentException("目标数量必须大于0", nameof(targetQuantity));

        if (targetPrice <= 0)
            throw new ArgumentException("目标单价必须大于0", nameof(targetPrice));

        ProductId = productId;
        TargetQuantity = targetQuantity;
        TargetPrice = targetPrice;
    }
}

public enum PlanStatus
{
    Draft = 0,
    Submitted = 1,
    Active = 2,
    Rejected = 3,
    Completed = 4
}