using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionException : AuditableEntity
{
    public int WorkOrderId { get; private set; }
    public ExceptionType Type { get; private set; }
    public ExceptionSeverity Severity { get; private set; }
    public ExceptionStatus Status { get; private set; }
    public string Description { get; private set; }
    public DateTime ReportTime { get; private set; }
    public DateTime? ResolvedTime { get; private set; }
    public string Resolution { get; private set; }
    public int? ResponsiblePersonId { get; private set; }
    public decimal? ProductionLoss { get; private set; }
    public string Notes { get; private set; }

    private ProductionException() { }

    public static ProductionException Create(
        int workOrderId,
        ExceptionType type,
        ExceptionSeverity severity,
        string description,
        int? responsiblePersonId = null,
        decimal? productionLoss = null,
        string notes = null)
    {
        if (workOrderId <= 0)
            throw new ArgumentException("工单ID必须大于0", nameof(workOrderId));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("异常描述不能为空", nameof(description));

        if (productionLoss.HasValue && productionLoss.Value < 0)
            throw new ArgumentException("生产损失不能为负数", nameof(productionLoss));

        return new ProductionException
        {
            WorkOrderId = workOrderId,
            Type = type,
            Severity = severity,
            Status = ExceptionStatus.Open,
            Description = description,
            ReportTime = DateTime.Now,
            ResponsiblePersonId = responsiblePersonId,
            ProductionLoss = productionLoss,
            Notes = notes
        };
    }

    public void UpdateStatus(ExceptionStatus newStatus, string resolution = null)
    {
        if (newStatus == Status)
            return;

        ValidateStatusTransition(newStatus);

        if (newStatus == ExceptionStatus.Resolved)
        {
            if (string.IsNullOrWhiteSpace(resolution))
                throw new ArgumentException("解决方案不能为空", nameof(resolution));

            Resolution = resolution;
            ResolvedTime = DateTime.Now;
        }

        Status = newStatus;
    }

    public void UpdateSeverity(ExceptionSeverity newSeverity)
    {
        if (Status == ExceptionStatus.Resolved)
            throw new InvalidOperationException("已解决的异常不能修改严重程度");

        Severity = newSeverity;
    }

    private void ValidateStatusTransition(ExceptionStatus newStatus)
    {
        switch (Status)
        {
            case ExceptionStatus.Open when newStatus != ExceptionStatus.InProgress && newStatus != ExceptionStatus.Resolved:
                throw new InvalidOperationException("未处理状态只能转换为处理中或已解决状态");
            case ExceptionStatus.InProgress when newStatus != ExceptionStatus.Resolved:
                throw new InvalidOperationException("处理中状态只能转换为已解决状态");
            case ExceptionStatus.Resolved:
                throw new InvalidOperationException("已解决状态不能转换为其他状态");
        }
    }
}

public enum ExceptionType
{
    Equipment = 1,    // 设备故障
    Material = 2,     // 物料短缺
    Quality = 3,      // 质量异常
    Process = 4,      // 工艺异常
    Personnel = 5     // 人员异常
}

public enum ExceptionSeverity
{
    Low = 1,         // 低
    Medium = 2,       // 中
    High = 3,         // 高
    Critical = 4      // 严重
}

public enum ExceptionStatus
{
    Open = 1,        // 未处理
    InProgress = 2,   // 处理中
    Resolved = 3      // 已解决
}