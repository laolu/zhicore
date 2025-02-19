using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionSchedule : AuditableEntity
{
    public int WorkOrderId { get; private set; }
    public DateTime ScheduledStartTime { get; private set; }
    public DateTime ScheduledEndTime { get; private set; }
    public int EquipmentId { get; private set; }
    public int WorkShiftId { get; private set; }
    public ScheduleStatus Status { get; private set; }
    public string Notes { get; private set; }
    
    private readonly List<ResourceAllocation> _resourceAllocations = new();
    public IReadOnlyCollection<ResourceAllocation> ResourceAllocations => _resourceAllocations.AsReadOnly();

    private ProductionSchedule() { }

    public static ProductionSchedule Create(
        int workOrderId,
        DateTime scheduledStartTime,
        DateTime scheduledEndTime,
        int equipmentId,
        int workShiftId,
        string notes = null)
    {
        if (workOrderId <= 0)
            throw new ArgumentException("工单ID必须大于0", nameof(workOrderId));

        if (scheduledEndTime <= scheduledStartTime)
            throw new ArgumentException("计划结束时间必须晚于开始时间", nameof(scheduledEndTime));

        if (equipmentId <= 0)
            throw new ArgumentException("设备ID必须大于0", nameof(equipmentId));

        if (workShiftId <= 0)
            throw new ArgumentException("班次ID必须大于0", nameof(workShiftId));

        return new ProductionSchedule
        {
            WorkOrderId = workOrderId,
            ScheduledStartTime = scheduledStartTime,
            ScheduledEndTime = scheduledEndTime,
            EquipmentId = equipmentId,
            WorkShiftId = workShiftId,
            Status = ScheduleStatus.Planned,
            Notes = notes
        };
    }

    public void AddResourceAllocation(
        int resourceId,
        ResourceType resourceType,
        decimal quantity,
        string notes = null)
    {
        var allocation = ResourceAllocation.Create(
            resourceId,
            resourceType,
            quantity,
            notes);

        _resourceAllocations.Add(allocation);
    }

    public void UpdateStatus(ScheduleStatus newStatus)
    {
        if (newStatus == Status)
            return;

        ValidateStatusTransition(newStatus);
        Status = newStatus;
    }

    private void ValidateStatusTransition(ScheduleStatus newStatus)
    {
        switch (Status)
        {
            case ScheduleStatus.Planned when newStatus != ScheduleStatus.InProgress:
                throw new InvalidOperationException("计划状态只能转换为进行中状态");
            case ScheduleStatus.InProgress when newStatus != ScheduleStatus.Completed && newStatus != ScheduleStatus.Cancelled:
                throw new InvalidOperationException("进行中状态只能转换为完成或取消状态");
            case ScheduleStatus.Completed:
            case ScheduleStatus.Cancelled:
                throw new InvalidOperationException("完成或取消状态不能转换为其他状态");
        }
    }
}

public enum ScheduleStatus
{
    Planned = 1,     // 已计划
    InProgress = 2,  // 进行中
    Completed = 3,   // 已完成
    Cancelled = 4    // 已取消
}

public enum ResourceType
{
    Material = 1,    // 物料
    Labor = 2,       // 人力
    Tool = 3         // 工具
}