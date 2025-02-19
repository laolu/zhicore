using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Common.Events;

namespace ZhiCore.Domain.Production;

public class ProductionPlanVersion : AuditableEntity
{
    public class PlanVersionStatusChangedEvent : DomainEvent
    {
        public string PlanNumber { get; }
        public string VersionNumber { get; }
        public VersionStatus OldStatus { get; }
        public VersionStatus NewStatus { get; }

        public PlanVersionStatusChangedEvent(string planNumber, string versionNumber, VersionStatus oldStatus, VersionStatus newStatus)
        {
            PlanNumber = planNumber;
            VersionNumber = versionNumber;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }

    public string PlanNumber { get; private set; }
    public string VersionNumber { get; private set; }
    public DateTime PlanDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public VersionStatus Status { get; private set; }
    public string Description { get; private set; }
    public string ApprovedBy { get; private set; }
    public DateTime? ApprovalDate { get; private set; }
    public string ChangeReason { get; private set; }
    private readonly List<PlanItemVersion> _items = new();
    public IReadOnlyCollection<PlanItemVersion> Items => _items.AsReadOnly();

    private ProductionPlanVersion() { }

    public static ProductionPlanVersion Create(
        string planNumber,
        string versionNumber,
        DateTime planDate,
        DateTime startDate,
        DateTime endDate,
        string description = null,
        string changeReason = null)
    {
        if (string.IsNullOrWhiteSpace(planNumber))
            throw new ArgumentException("计划编号不能为空", nameof(planNumber));

        if (string.IsNullOrWhiteSpace(versionNumber))
            throw new ArgumentException("版本号不能为空", nameof(versionNumber));

        if (endDate <= startDate)
            throw new ArgumentException("结束日期必须晚于开始日期", nameof(endDate));

        return new ProductionPlanVersion
        {
            PlanNumber = planNumber,
            VersionNumber = versionNumber,
            PlanDate = planDate,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            Status = VersionStatus.Draft,
            ChangeReason = changeReason
        };
    }

    public void AddItem(
        string productCode,
        int quantity,
        DateTime requiredDate,
        string routeCode,
        string description = null,
        int safetyStock = 0,
        int minimumOrderQuantity = 0,
        int leadTimeDays = 0,
        string bomVersion = null)
    {
        if (Status != VersionStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的版本可以添加项目");

        var item = PlanItemVersion.Create(
            productCode,
            quantity,
            requiredDate,
            routeCode,
            description,
            safetyStock,
            minimumOrderQuantity,
            leadTimeDays,
            bomVersion);

        _items.Add(item);
    }

    public void RemoveItem(string productCode)
    {
        if (Status != VersionStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的版本可以删除项目");

        var item = _items.Find(i => i.ProductCode == productCode);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public ProductionPlanVersion Copy(string newVersionNumber, string changeReason = null)
    {
        var newVersion = Create(
            PlanNumber,
            newVersionNumber,
            DateTime.Now,
            StartDate,
            EndDate,
            Description,
            changeReason);

        foreach (var item in _items)
        {
            newVersion.AddItem(
                item.ProductCode,
                item.Quantity,
                item.RequiredDate,
                item.RouteCode,
                item.Description,
                item.SafetyStock,
                item.MinimumOrderQuantity,
                item.LeadTimeDays,
                item.BomVersion);
        }

        return newVersion;
    }

    public void Submit()
    {
        if (Status != VersionStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的版本可以提交");

        if (_items.Count == 0)
            throw new InvalidOperationException("计划版本必须包含至少一个项目");

        var oldStatus = Status;
        Status = VersionStatus.Submitted;
        AddDomainEvent(new PlanVersionStatusChangedEvent(PlanNumber, VersionNumber, oldStatus, Status));
    }

    public void Approve(string approvedBy)
    {
        if (Status != VersionStatus.Submitted)
            throw new InvalidOperationException("只有已提交的版本可以审批");

        if (string.IsNullOrWhiteSpace(approvedBy))
            throw new ArgumentException("审批人不能为空", nameof(approvedBy));

        var oldStatus = Status;
        Status = VersionStatus.Approved;
        ApprovedBy = approvedBy;
        ApprovalDate = DateTime.Now;
        AddDomainEvent(new PlanVersionStatusChangedEvent(PlanNumber, VersionNumber, oldStatus, Status));
    }

    public void Reject(string reason)
    {
        if (Status != VersionStatus.Submitted)
            throw new InvalidOperationException("只有已提交的版本可以驳回");

        var oldStatus = Status;
        Status = VersionStatus.Rejected;
        Description += $"\n驳回原因：{reason}";
        AddDomainEvent(new PlanVersionStatusChangedEvent(PlanNumber, VersionNumber, oldStatus, Status));
    }
}

public class PlanItemVersion : Entity
{
    public string ProductCode { get; private set; }
    public int Quantity { get; private set; }
    public DateTime RequiredDate { get; private set; }
    public string RouteCode { get; private set; }
    public string Description { get; private set; }
    public string ChangeReason { get; private set; }
    // MRP相关属性
    public int SafetyStock { get; private set; }
    public int MinimumOrderQuantity { get; private set; }
    public int LeadTimeDays { get; private set; }
    public string BomVersion { get; private set; }

    private PlanItemVersion() { }

    public static PlanItemVersion Create(
        string productCode,
        int quantity,
        DateTime requiredDate,
        string routeCode,
        string description = null,
        int safetyStock = 0,
        int minimumOrderQuantity = 0,
        int leadTimeDays = 0,
        string bomVersion = null)
    {
        return new PlanItemVersion
        {
            ProductCode = productCode,
            Quantity = quantity,
            RequiredDate = requiredDate,
            RouteCode = routeCode,
            Description = description,
            SafetyStock = safetyStock,
            MinimumOrderQuantity = minimumOrderQuantity,
            LeadTimeDays = leadTimeDays,
            BomVersion = bomVersion
        };
    }
}