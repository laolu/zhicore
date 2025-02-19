using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionPlan : AuditableEntity, IAggregateRoot
{
    public string PlanNumber { get; private set; }
    public DateTime PlanDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public PlanStatus Status { get; private set; }
    public string Description { get; private set; }
    private readonly List<PlanItem> _items = new();
    public IReadOnlyCollection<PlanItem> Items => _items.AsReadOnly();

    private ProductionPlan() { }

    public static ProductionPlan Create(
        DateTime planDate,
        DateTime startDate,
        DateTime endDate,
        string description = null)
    {
        var plan = new ProductionPlan
        {
            PlanNumber = GeneratePlanNumber(),
            PlanDate = planDate,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            Status = PlanStatus.Draft
        };

        return plan;
    }

    public void AddItem(
        string productCode,
        int quantity,
        DateTime requiredDate,
        string routeCode,
        string description = null)
    {
        if (Status != PlanStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的计划可以添加项目");

        var item = PlanItem.Create(
            productCode,
            quantity,
            requiredDate,
            routeCode,
            description);

        _items.Add(item);
    }

    public void RemoveItem(string productCode)
    {
        if (Status != PlanStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的计划可以删除项目");

        var item = _items.Find(i => i.ProductCode == productCode);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void Confirm()
    {
        if (Status != PlanStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的计划可以确认");

        if (_items.Count == 0)
            throw new InvalidOperationException("计划中没有项目");

        Status = PlanStatus.Confirmed;
    }

    public void Cancel(string reason)
    {
        if (Status == PlanStatus.Completed || Status == PlanStatus.Cancelled)
            throw new InvalidOperationException("已完成或已取消的计划不能取消");

        Status = PlanStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != PlanStatus.Confirmed)
            throw new InvalidOperationException("只有已确认的计划可以完成");

        Status = PlanStatus.Completed;
    }

    private static string GeneratePlanNumber()
    {
        return $"PP{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public class PlanItem : AuditableEntity
{
    public string ProductCode { get; private set; }
    public int Quantity { get; private set; }
    public DateTime RequiredDate { get; private set; }
    public string RouteCode { get; private set; }
    public string Description { get; private set; }

    private PlanItem() { }

    public static PlanItem Create(
        string productCode,
        int quantity,
        DateTime requiredDate,
        string routeCode,
        string description = null)
    {
        return new PlanItem
        {
            ProductCode = productCode,
            Quantity = quantity,
            RequiredDate = requiredDate,
            RouteCode = routeCode,
            Description = description
        };
    }
}

public enum PlanStatus
{
    Draft = 0,
    Confirmed = 1,
    Completed = 2,
    Cancelled = 3
}