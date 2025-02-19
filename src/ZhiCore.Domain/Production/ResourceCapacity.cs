using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ResourceCapacity : AuditableEntity
{
    public string ResourceCode { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal AvailableHours { get; private set; }  // 可用工时
    public decimal PlannedHours { get; private set; }    // 计划工时
    public decimal LoadRate { get; private set; }        // 负荷率
    public string Notes { get; private set; }
    private readonly List<CapacityDetail> _details = new();
    public IReadOnlyCollection<CapacityDetail> Details => _details.AsReadOnly();

    private ResourceCapacity() { }

    public static ResourceCapacity Create(
        string resourceCode,
        DateTime startDate,
        DateTime endDate,
        decimal availableHours,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(resourceCode))
            throw new ArgumentException("资源编码不能为空", nameof(resourceCode));

        if (endDate <= startDate)
            throw new ArgumentException("结束日期必须晚于开始日期", nameof(endDate));

        if (availableHours <= 0)
            throw new ArgumentException("可用工时必须大于0", nameof(availableHours));

        return new ResourceCapacity
        {
            ResourceCode = resourceCode,
            StartDate = startDate,
            EndDate = endDate,
            AvailableHours = availableHours,
            PlannedHours = 0,
            LoadRate = 0,
            Notes = notes
        };
    }

    public void AddCapacityDetail(
        string workOrderNumber,
        decimal plannedHours,
        DateTime startTime,
        DateTime endTime,
        string description = null)
    {
        var detail = CapacityDetail.Create(
            workOrderNumber,
            plannedHours,
            startTime,
            endTime,
            description);

        _details.Add(detail);
        UpdateCapacity();
    }

    public void RemoveCapacityDetail(string workOrderNumber)
    {
        var detail = _details.Find(d => d.WorkOrderNumber == workOrderNumber);
        if (detail != null)
        {
            _details.Remove(detail);
            UpdateCapacity();
        }
    }

    private void UpdateCapacity()
    {
        PlannedHours = 0;
        foreach (var detail in _details)
        {
            PlannedHours += detail.PlannedHours;
        }

        LoadRate = PlannedHours / AvailableHours * 100;
    }
}

public class CapacityDetail : Entity
{
    public string WorkOrderNumber { get; private set; }
    public decimal PlannedHours { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public string Description { get; private set; }

    private CapacityDetail() { }

    public static CapacityDetail Create(
        string workOrderNumber,
        decimal plannedHours,
        DateTime startTime,
        DateTime endTime,
        string description = null)
    {
        if (string.IsNullOrWhiteSpace(workOrderNumber))
            throw new ArgumentException("工单编号不能为空", nameof(workOrderNumber));

        if (plannedHours <= 0)
            throw new ArgumentException("计划工时必须大于0", nameof(plannedHours));

        if (endTime <= startTime)
            throw new ArgumentException("结束时间必须晚于开始时间", nameof(endTime));

        return new CapacityDetail
        {
            WorkOrderNumber = workOrderNumber,
            PlannedHours = plannedHours,
            StartTime = startTime,
            EndTime = endTime,
            Description = description
        };
    }
}