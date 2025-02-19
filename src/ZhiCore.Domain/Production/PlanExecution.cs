using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class PlanExecution : AuditableEntity
{
    public int ProductionPlanId { get; private set; }
    public DateTime ExecutionDate { get; private set; }
    public decimal PlanProgress { get; private set; }  // 计划进度百分比
    public decimal ActualProgress { get; private set; }  // 实际进度百分比
    public decimal CompletionRate { get; private set; }  // 计划达成率
    public int TotalExceptions { get; private set; }  // 异常总数
    public decimal QualityRate { get; private set; }  // 质量合格率
    public ExecutionStatus Status { get; private set; }
    public string Notes { get; private set; }
    
    private readonly List<ExecutionKPI> _kpis = new();
    public IReadOnlyCollection<ExecutionKPI> KPIs => _kpis.AsReadOnly();

    private PlanExecution() { }

    public static PlanExecution Create(
        int productionPlanId,
        DateTime executionDate,
        decimal planProgress,
        decimal actualProgress,
        decimal qualityRate,
        int totalExceptions = 0,
        string notes = null)
    {
        if (productionPlanId <= 0)
            throw new ArgumentException("生产计划ID必须大于0", nameof(productionPlanId));

        if (planProgress < 0 || planProgress > 100)
            throw new ArgumentException("计划进度必须在0到100之间", nameof(planProgress));

        if (actualProgress < 0 || actualProgress > 100)
            throw new ArgumentException("实际进度必须在0到100之间", nameof(actualProgress));

        if (qualityRate < 0 || qualityRate > 100)
            throw new ArgumentException("质量合格率必须在0到100之间", nameof(qualityRate));

        if (totalExceptions < 0)
            throw new ArgumentException("异常数量不能为负数", nameof(totalExceptions));

        var completionRate = planProgress > 0 ? (actualProgress / planProgress) * 100 : 0;

        return new PlanExecution
        {
            ProductionPlanId = productionPlanId,
            ExecutionDate = executionDate,
            PlanProgress = planProgress,
            ActualProgress = actualProgress,
            CompletionRate = completionRate,
            QualityRate = qualityRate,
            TotalExceptions = totalExceptions,
            Status = ExecutionStatus.InProgress,
            Notes = notes
        };
    }

    public void UpdateProgress(
        decimal newPlanProgress,
        decimal newActualProgress,
        decimal newQualityRate)
    {
        if (newPlanProgress < 0 || newPlanProgress > 100)
            throw new ArgumentException("计划进度必须在0到100之间", nameof(newPlanProgress));

        if (newActualProgress < 0 || newActualProgress > 100)
            throw new ArgumentException("实际进度必须在0到100之间", nameof(newActualProgress));

        if (newQualityRate < 0 || newQualityRate > 100)
            throw new ArgumentException("质量合格率必须在0到100之间", nameof(newQualityRate));

        PlanProgress = newPlanProgress;
        ActualProgress = newActualProgress;
        QualityRate = newQualityRate;
        CompletionRate = newPlanProgress > 0 ? (newActualProgress / newPlanProgress) * 100 : 0;

        UpdateStatus();
    }

    public void AddException()
    {
        TotalExceptions++;
    }

    public void AddKPI(
        string name,
        decimal target,
        decimal actual,
        string unit,
        string notes = null)
    {
        var kpi = ExecutionKPI.Create(
            name,
            target,
            actual,
            unit,
            notes);

        _kpis.Add(kpi);
    }

    private void UpdateStatus()
    {
        if (ActualProgress >= 100)
        {
            Status = ExecutionStatus.Completed;
        }
        else if (ActualProgress < PlanProgress)
        {
            Status = ExecutionStatus.Delayed;
        }
        else
        {
            Status = ExecutionStatus.InProgress;
        }
    }
}

public enum ExecutionStatus
{
    InProgress = 1,  // 进行中
    Delayed = 2,     // 延迟
    Completed = 3    // 已完成
}

public class ExecutionKPI
{
    public string Name { get; private set; }
    public decimal Target { get; private set; }
    public decimal Actual { get; private set; }
    public string Unit { get; private set; }
    public decimal AchievementRate { get; private set; }
    public string Notes { get; private set; }

    private ExecutionKPI() { }

    public static ExecutionKPI Create(
        string name,
        decimal target,
        decimal actual,
        string unit,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("KPI名称不能为空", nameof(name));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("单位不能为空", nameof(unit));

        var achievementRate = target > 0 ? (actual / target) * 100 : 0;

        return new ExecutionKPI
        {
            Name = name,
            Target = target,
            Actual = actual,
            Unit = unit,
            AchievementRate = achievementRate,
            Notes = notes
        };
    }

    public void UpdateActual(decimal newActual)
    {
        Actual = newActual;
        AchievementRate = Target > 0 ? (newActual / Target) * 100 : 0;
    }
}