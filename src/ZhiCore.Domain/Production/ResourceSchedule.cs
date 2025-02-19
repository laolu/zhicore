using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ResourceSchedule : AuditableEntity
{
    public int ResourceId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public string WorkOrderCode { get; private set; }
    public string OperationCode { get; private set; }
    public ScheduleStatus Status { get; private set; }
    public string Notes { get; private set; }
    
    private ResourceSchedule() { }

    public static ResourceSchedule Create(
        int resourceId,
        DateTime startTime,
        DateTime endTime,
        string workOrderCode,
        string operationCode,
        string notes = null)
    {
        if (resourceId <= 0)
            throw new ArgumentException("资源ID必须大于0", nameof(resourceId));

        if (endTime <= startTime)
            throw new ArgumentException("结束时间必须晚于开始时间");

        if (string.IsNullOrWhiteSpace(workOrderCode))
            throw new ArgumentException("工单编码不能为空", nameof(workOrderCode));

        if (string.IsNullOrWhiteSpace(operationCode))
            throw new ArgumentException("工序编码不能为空", nameof(operationCode));

        return new ResourceSchedule
        {
            ResourceId = resourceId,
            StartTime = startTime,
            EndTime = endTime,
            WorkOrderCode = workOrderCode,
            OperationCode = operationCode,
            Status = ScheduleStatus.Planned,
            Notes = notes
        };
    }

    public void UpdateStatus(ScheduleStatus newStatus)
    {
        if (newStatus == Status)
            return;

        ValidateStatusTransition(newStatus);
        Status = newStatus;
    }

    public void UpdateTimeRange(DateTime newStartTime, DateTime newEndTime)
    {
        if (newEndTime <= newStartTime)
            throw new ArgumentException("结束时间必须晚于开始时间");

        if (Status != ScheduleStatus.Planned)
            throw new InvalidOperationException("只有计划状态的排班才能修改时间");

        StartTime = newStartTime;
        EndTime = newEndTime;
    }

    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
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