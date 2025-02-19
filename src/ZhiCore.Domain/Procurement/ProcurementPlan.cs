using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class ProcurementPlan : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public PlanStatus Status { get; private set; }
    public string Description { get; private set; }
    public decimal TotalAmount { get; private set; }
    
    private readonly List<ProcurementPlanItem> _items = new();
    public IReadOnlyCollection<ProcurementPlanItem> Items => _items.AsReadOnly();

    private ProcurementPlan() { }

    public static ProcurementPlan Create(
        string code,
        string name,
        DateTime startDate,
        DateTime endDate,
        string description = null)
    {
        if (startDate >= endDate)
            throw new InvalidOperationException("计划开始日期必须早于结束日期");

        return new ProcurementPlan
        {
            Code = code,
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            Status = PlanStatus.Draft,
            TotalAmount = 0
        };
    }

    public void AddItem(
        string materialCode,
        string materialName,
        decimal quantity,
        string unit,
        decimal estimatedPrice,
        DateTime expectedDeliveryDate,
        string remark = null)
    {
        if (Status != PlanStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的计划可以添加项目");

        var item = new ProcurementPlanItem(
            materialCode,
            materialName,
            quantity,
            unit,
            estimatedPrice,
            expectedDeliveryDate,
            remark);

        _items.Add(item);
        UpdateTotalAmount();
    }

    public void RemoveItem(string materialCode)
    {
        if (Status != PlanStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的计划可以删除项目");

        var item = _items.Find(x => x.MaterialCode == materialCode);
        if (item != null)
        {
            _items.Remove(item);
            UpdateTotalAmount();
        }
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = 0;
        foreach (var item in _items)
        {
            TotalAmount += item.TotalAmount;
        }
    }

    public void Submit()
    {
        if (Status != PlanStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的计划可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("计划中必须包含至少一个采购项目");

        Status = PlanStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != PlanStatus.Submitted)
            throw new InvalidOperationException("只有已提交的计划可以审批");

        Status = PlanStatus.Approved;
    }

    public void Reject(string reason)
    {
        if (Status != PlanStatus.Submitted)
            throw new InvalidOperationException("只有已提交的计划可以驳回");

        Status = PlanStatus.Rejected;
    }

    public void Cancel(string reason)
    {
        if (Status == PlanStatus.Completed || Status == PlanStatus.Cancelled)
            throw new InvalidOperationException("已完成或已取消的计划不能取消");

        Status = PlanStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != PlanStatus.Approved)
            throw new InvalidOperationException("只有已审批的计划可以完成");

        Status = PlanStatus.Completed;
    }
}

public enum PlanStatus
{
    Draft = 1,      // 草稿
    Submitted = 2,  // 已提交
    Approved = 3,   // 已审批
    Rejected = 4,   // 已驳回
    Completed = 5,  // 已完成
    Cancelled = 6   // 已取消
}