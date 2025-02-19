using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class Tooling : AuditableEntity
{
    public string ToolingCode { get; private set; }
    public string Name { get; private set; }
    public string Specification { get; private set; }
    public string Category { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public int ExpectedLifespan { get; private set; }
    public int CurrentUsage { get; private set; }
    public ToolingStatus Status { get; private set; }
    public string Location { get; private set; }
    public string Notes { get; private set; }
    
    private readonly List<ToolingUsageRecord> _usageRecords = new();
    public IReadOnlyCollection<ToolingUsageRecord> UsageRecords => _usageRecords.AsReadOnly();

    private Tooling() { }

    public static Tooling Create(
        string toolingCode,
        string name,
        string specification,
        string category,
        DateTime purchaseDate,
        int expectedLifespan,
        string location,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(toolingCode))
            throw new ArgumentException("工装编号不能为空", nameof(toolingCode));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("工装名称不能为空", nameof(name));

        if (expectedLifespan <= 0)
            throw new ArgumentException("预期寿命必须大于0", nameof(expectedLifespan));

        return new Tooling
        {
            ToolingCode = toolingCode,
            Name = name,
            Specification = specification,
            Category = category,
            PurchaseDate = purchaseDate,
            ExpectedLifespan = expectedLifespan,
            CurrentUsage = 0,
            Status = ToolingStatus.Available,
            Location = location,
            Notes = notes
        };
    }

    public void AddUsageRecord(
        int productionTaskId,
        int usageCount,
        string notes = null)
    {
        if (Status != ToolingStatus.Available)
            throw new InvalidOperationException("只有可用状态的工装才能使用");

        var record = ToolingUsageRecord.Create(
            productionTaskId,
            usageCount,
            notes);

        _usageRecords.Add(record);
        CurrentUsage += usageCount;

        if (CurrentUsage >= ExpectedLifespan)
        {
            Status = ToolingStatus.NeedsMaintenance;
        }
    }

    public void UpdateStatus(ToolingStatus newStatus)
    {
        if (newStatus == Status)
            return;

        Status = newStatus;

        if (newStatus == ToolingStatus.Available)
        {
            CurrentUsage = 0;
        }
    }
}

public enum ToolingStatus
{
    Available = 1,           // 可用
    InUse = 2,               // 使用中
    NeedsMaintenance = 3,    // 需要维护
    UnderMaintenance = 4,    // 维护中
    Scrapped = 5             // 报废
}

public class ToolingUsageRecord : AuditableEntity
{
    public int ProductionTaskId { get; private set; }
    public int UsageCount { get; private set; }
    public string Notes { get; private set; }

    private ToolingUsageRecord() { }

    public static ToolingUsageRecord Create(
        int productionTaskId,
        int usageCount,
        string notes = null)
    {
        if (productionTaskId <= 0)
            throw new ArgumentException("生产任务ID必须大于0", nameof(productionTaskId));

        if (usageCount <= 0)
            throw new ArgumentException("使用次数必须大于0", nameof(usageCount));

        return new ToolingUsageRecord
        {
            ProductionTaskId = productionTaskId,
            UsageCount = usageCount,
            Notes = notes
        };
    }
}