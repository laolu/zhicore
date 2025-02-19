using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionTask : AuditableEntity
{
    public string TaskNumber { get; private set; }
    public string WorkstationCode { get; private set; }
    public string OperationCode { get; private set; }
    public string OperationName { get; private set; }
    public DateTime PlannedStartTime { get; private set; }
    public DateTime PlannedEndTime { get; private set; }
    public DateTime? ActualStartTime { get; private set; }
    public DateTime? ActualEndTime { get; private set; }
    public TaskStatus Status { get; private set; }
    public string Description { get; private set; }

    private ProductionTask() { }

    public static ProductionTask Create(
        string workstationCode,
        string operationCode,
        string operationName,
        DateTime plannedStartTime,
        DateTime plannedEndTime,
        string description = null)
    {
        var task = new ProductionTask
        {
            TaskNumber = GenerateTaskNumber(),
            WorkstationCode = workstationCode,
            OperationCode = operationCode,
            OperationName = operationName,
            PlannedStartTime = plannedStartTime,
            PlannedEndTime = plannedEndTime,
            Description = description,
            Status = TaskStatus.Pending
        };

        return task;
    }

    public void Start()
    {
        if (Status != TaskStatus.Pending)
            throw new InvalidOperationException("只有待处理状态的任务可以开始");

        Status = TaskStatus.InProgress;
        ActualStartTime = DateTime.Now;
    }

    public void Complete()
    {
        if (Status != TaskStatus.InProgress)
            throw new InvalidOperationException("只有进行中的任务可以完成");

        Status = TaskStatus.Completed;
        ActualEndTime = DateTime.Now;
    }

    public void Pause(string reason)
    {
        if (Status != TaskStatus.InProgress)
            throw new InvalidOperationException("只有进行中的任务可以暂停");

        Status = TaskStatus.Paused;
    }

    public void Resume()
    {
        if (Status != TaskStatus.Paused)
            throw new InvalidOperationException("只有暂停状态的任务可以恢复");

        Status = TaskStatus.InProgress;
    }

    private static string GenerateTaskNumber()
    {
        return $"PT{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum TaskStatus
{
    Pending = 0,
    InProgress = 1,
    Paused = 2,
    Completed = 3
}