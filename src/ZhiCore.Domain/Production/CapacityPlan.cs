using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class CapacityPlan : AuditableEntity
{
    public string PlanCode { get; private set; }
    public DateTime PlanDate { get; private set; }
    public string Workstation { get; private set; }
    public decimal AvailableHours { get; private set; }
    public decimal PlannedHours { get; private set; }
    public decimal ActualHours { get; private set; }
    public string Notes { get; private set; }
    
    private readonly List<CapacityPlanDetail> _details = new();
    public IReadOnlyCollection<CapacityPlanDetail> Details => _details.AsReadOnly();

    private CapacityPlan() { }

    public static CapacityPlan Create(
        string planCode,
        DateTime planDate,
        string workstation,
        decimal availableHours,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(planCode))
            throw new ArgumentException("计划编号不能为空", nameof(planCode));

        if (string.IsNullOrWhiteSpace(workstation))
            throw new ArgumentException("工作中心不能为空", nameof(workstation));

        if (availableHours <= 0)
            throw new ArgumentException("可用工时必须大于0", nameof(availableHours));

        return new CapacityPlan
        {
            PlanCode = planCode,
            PlanDate = planDate,
            Workstation = workstation,
            AvailableHours = availableHours,
            PlannedHours = 0,
            ActualHours = 0,
            Notes = notes
        };
    }

    public void AddDetail(
        int workOrderId,
        string operationCode,
        decimal plannedHours)
    {
        if (plannedHours <= 0)
            throw new ArgumentException("计划工时必须大于0", nameof(plannedHours));

        if (PlannedHours + plannedHours > AvailableHours)
            throw new InvalidOperationException("计划工时不能超过可用工时");

        var detail = CapacityPlanDetail.Create(
            workOrderId,
            operationCode,
            plannedHours);

        _details.Add(detail);
        PlannedHours += plannedHours;
    }

    public void UpdateActualHours(decimal actualHours)
    {
        if (actualHours < 0)
            throw new ArgumentException("实际工时不能为负数", nameof(actualHours));

        ActualHours = actualHours;
    }
}

public class CapacityPlanDetail : Entity
{
    public int WorkOrderId { get; private set; }
    public string OperationCode { get; private set; }
    public decimal PlannedHours { get; private set; }
    public decimal? ActualHours { get; private set; }

    private CapacityPlanDetail() { }

    public static CapacityPlanDetail Create(
        int workOrderId,
        string operationCode,
        decimal plannedHours)
    {
        if (workOrderId <= 0)
            throw new ArgumentException("工单ID必须大于0", nameof(workOrderId));

        if (string.IsNullOrWhiteSpace(operationCode))
            throw new ArgumentException("工序编码不能为空", nameof(operationCode));

        if (plannedHours <= 0)
            throw new ArgumentException("计划工时必须大于0", nameof(plannedHours));

        return new CapacityPlanDetail
        {
            WorkOrderId = workOrderId,
            OperationCode = operationCode,
            PlannedHours = plannedHours
        };
    }

    public void UpdateActualHours(decimal actualHours)
    {
        if (actualHours < 0)
            throw new ArgumentException("实际工时不能为负数", nameof(actualHours));

        ActualHours = actualHours;
    }
}