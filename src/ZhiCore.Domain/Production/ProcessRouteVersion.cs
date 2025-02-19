using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProcessRouteVersion : AuditableEntity
{
    public string RouteCode { get; private set; }
    public string VersionNumber { get; private set; }
    public string Description { get; private set; }
    public VersionStatus Status { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string ApprovedBy { get; private set; }
    public DateTime? ApprovalDate { get; private set; }
    public string ChangeReason { get; private set; }
    private readonly List<ProcessStepVersion> _steps = new();
    public IReadOnlyCollection<ProcessStepVersion> Steps => _steps.AsReadOnly();

    private ProcessRouteVersion() { }

    public static ProcessRouteVersion Create(
        string routeCode,
        string versionNumber,
        string description,
        DateTime effectiveDate,
        string changeReason = null)
    {
        if (string.IsNullOrWhiteSpace(routeCode))
            throw new ArgumentException("工艺路线编码不能为空", nameof(routeCode));

        if (string.IsNullOrWhiteSpace(versionNumber))
            throw new ArgumentException("版本号不能为空", nameof(versionNumber));

        return new ProcessRouteVersion
        {
            RouteCode = routeCode,
            VersionNumber = versionNumber,
            Description = description,
            Status = VersionStatus.Draft,
            EffectiveDate = effectiveDate,
            ChangeReason = changeReason
        };
    }

    public void AddStep(ProcessStepVersion step)
    {
        if (Status != VersionStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的版本可以添加工序");

        _steps.Add(step);
    }

    public void RemoveStep(ProcessStepVersion step)
    {
        if (Status != VersionStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的版本可以删除工序");

        _steps.Remove(step);
    }

    public void Submit()
    {
        if (Status != VersionStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的版本可以提交");

        if (_steps.Count == 0)
            throw new InvalidOperationException("工艺路线版本必须包含至少一个工序");

        Status = VersionStatus.Submitted;
    }

    public void Approve(string approvedBy)
    {
        if (Status != VersionStatus.Submitted)
            throw new InvalidOperationException("只有已提交的版本可以审批");

        if (string.IsNullOrWhiteSpace(approvedBy))
            throw new ArgumentException("审批人不能为空", nameof(approvedBy));

        Status = VersionStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovalDate = DateTime.Now;
    }

    public void Reject(string reason)
    {
        if (Status != VersionStatus.Submitted)
            throw new InvalidOperationException("只有已提交的版本可以驳回");

        Status = VersionStatus.Rejected;
        Description += $"\n驳回原因：{reason}";
    }

    public void Expire(DateTime expirationDate)
    {
        if (Status != VersionStatus.Approved)
            throw new InvalidOperationException("只有已审批的版本可以设置过期时间");

        if (expirationDate <= EffectiveDate)
            throw new ArgumentException("过期时间必须晚于生效时间", nameof(expirationDate));

        ExpirationDate = expirationDate;
        Status = VersionStatus.Expired;
    }
}

public class ProcessStepVersion : Entity
{
    public string StepCode { get; private set; }
    public string Name { get; private set; }
    public int Sequence { get; private set; }
    public string Description { get; private set; }
    public decimal StandardTime { get; private set; }
    public string QualityRequirements { get; private set; }
    public string EquipmentRequirements { get; private set; }
    public string ChangeReason { get; private set; }

    private ProcessStepVersion() { }

    public static ProcessStepVersion Create(
        string stepCode,
        string name,
        int sequence,
        decimal standardTime,
        string description = null,
        string qualityRequirements = null,
        string equipmentRequirements = null,
        string changeReason = null)
    {
        return new ProcessStepVersion
        {
            StepCode = stepCode,
            Name = name,
            Sequence = sequence,
            StandardTime = standardTime,
            Description = description,
            QualityRequirements = qualityRequirements,
            EquipmentRequirements = equipmentRequirements,
            ChangeReason = changeReason
        };
    }
}

public enum VersionStatus
{
    Draft = 1,      // 草稿
    Submitted = 2,   // 已提交
    Approved = 3,    // 已审批
    Rejected = 4,    // 已驳回
    Expired = 5      // 已过期
}