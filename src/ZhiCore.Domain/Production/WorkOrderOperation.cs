using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class WorkOrderOperation : AuditableEntity
{
    public string OperationCode { get; private set; }
    public string Name { get; private set; }
    public int Sequence { get; private set; }
    public decimal StandardTime { get; private set; }
    public string Workstation { get; private set; }
    public OperationStatus Status { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public decimal? ActualTime { get; private set; }
    public string OperatorId { get; private set; }
    public string Notes { get; private set; }

    private WorkOrderOperation() { }

    public static WorkOrderOperation Create(
        string operationCode,
        string name,
        int sequence,
        decimal standardTime,
        string workstation = null)
    {
        if (string.IsNullOrWhiteSpace(operationCode))
            throw new ArgumentException("工序编码不能为空", nameof(operationCode));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("工序名称不能为空", nameof(name));

        if (sequence <= 0)
            throw new ArgumentException("工序顺序必须大于0", nameof(sequence));

        if (standardTime <= 0)
            throw new ArgumentException("标准工时必须大于0", nameof(standardTime));

        return new WorkOrderOperation
        {
            OperationCode = operationCode,
            Name = name,
            Sequence = sequence,
            StandardTime = standardTime,
            Workstation = workstation,
            Status = OperationStatus.Pending
        };
    }

    public void Start(string operatorId)
    {
        if (Status != OperationStatus.Pending)
            throw new InvalidOperationException("只有待执行状态的工序可以开始");

        Status = OperationStatus.InProgress;
        StartTime = DateTime.Now;
        OperatorId = operatorId;
    }

    public void Complete(decimal actualTime)
    {
        if (Status != OperationStatus.InProgress)
            throw new InvalidOperationException("只有进行中状态的工序可以完成");

        if (actualTime <= 0)
            throw new ArgumentException("实际工时必须大于0", nameof(actualTime));

        Status = OperationStatus.Completed;
        EndTime = DateTime.Now;
        ActualTime = actualTime;
    }

    public void Suspend(string notes)
    {
        if (Status != OperationStatus.InProgress)
            throw new InvalidOperationException("只有进行中状态的工序可以暂停");

        Status = OperationStatus.Suspended;
        Notes = notes;
    }

    public void Resume()
    {
        if (Status != OperationStatus.Suspended)
            throw new InvalidOperationException("只有暂停状态的工序可以恢复");

        Status = OperationStatus.InProgress;
    }
}

public enum OperationStatus
{
    Pending = 1,     // 待执行
    InProgress = 2,  // 进行中
    Suspended = 3,   // 暂停
    Completed = 4    // 完成
}