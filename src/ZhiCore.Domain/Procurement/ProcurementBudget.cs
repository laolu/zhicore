using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Finance;

namespace ZhiCore.Domain.Procurement;

public class ProcurementBudget : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public int Year { get; private set; }
    public BudgetPeriodType PeriodType { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal UsedAmount { get; private set; }
    public decimal RemainingAmount { get; private set; }
    public BudgetStatus Status { get; private set; }
    public string Description { get; private set; }
    
    private readonly List<ProcurementBudgetItem> _items = new();
    public IReadOnlyCollection<ProcurementBudgetItem> Items => _items.AsReadOnly();

    private ProcurementBudget() { }

    public static ProcurementBudget Create(
        string code,
        string name,
        int year,
        BudgetPeriodType periodType,
        string description = null)
    {
        if (year < DateTime.Now.Year)
            throw new InvalidOperationException("预算年度不能小于当前年度");

        return new ProcurementBudget
        {
            Code = code,
            Name = name,
            Year = year,
            PeriodType = periodType,
            Description = description,
            Status = BudgetStatus.Draft,
            TotalAmount = 0,
            UsedAmount = 0,
            RemainingAmount = 0
        };
    }

    public void AddItem(
        string departmentCode,
        string departmentName,
        string categoryCode,
        string categoryName,
        decimal amount,
        string remark = null)
    {
        if (Status != BudgetStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的预算可以添加项目");

        if (amount <= 0)
            throw new InvalidOperationException("预算金额必须大于0");

        var item = new ProcurementBudgetItem(
            departmentCode,
            departmentName,
            categoryCode,
            categoryName,
            amount,
            remark);

        _items.Add(item);
        UpdateTotalAmount();
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = 0;
        foreach (var item in _items)
        {
            TotalAmount += item.Amount;
        }
        RemainingAmount = TotalAmount - UsedAmount;
    }

    public void Submit()
    {
        if (Status != BudgetStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的预算可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("预算必须包含至少一个项目");

        Status = BudgetStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != BudgetStatus.Submitted)
            throw new InvalidOperationException("只有已提交的预算可以审批");

        Status = BudgetStatus.Approved;
    }

    public void Reject(string reason)
    {
        if (Status != BudgetStatus.Submitted)
            throw new InvalidOperationException("只有已提交的预算可以驳回");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("驳回原因不能为空", nameof(reason));

        Status = BudgetStatus.Rejected;
        Description = reason;
    }

    public void UpdateUsedAmount(decimal amount)
    {
        if (Status != BudgetStatus.Approved)
            throw new InvalidOperationException("只有已审批的预算可以更新使用金额");

        if (amount < 0)
            throw new InvalidOperationException("使用金额不能为负数");

        if (amount > RemainingAmount)
            throw new InvalidOperationException("使用金额不能超过剩余预算");

        UsedAmount += amount;
        RemainingAmount = TotalAmount - UsedAmount;
    }
}

public enum BudgetPeriodType
{
    Annual = 1,    // 年度
    Quarterly = 2, // 季度
    Monthly = 3    // 月度
}

public enum BudgetStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3
}