using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryQuality : AuditableEntity
{
    public int InventoryItemId { get; private set; }
    public QualityStatus Status { get; private set; }
    public string InspectorId { get; private set; }
    public DateTime InspectionDate { get; private set; }
    public string Notes { get; private set; }
    private readonly List<QualityDefect> _defects = new();
    public IReadOnlyList<QualityDefect> Defects => _defects.AsReadOnly();

    private InventoryQuality() { }

    public static InventoryQuality Create(
        int inventoryItemId,
        string inspectorId,
        string notes = null)
    {
        if (inventoryItemId <= 0)
            throw new ArgumentException("库存项ID必须大于0", nameof(inventoryItemId));

        if (string.IsNullOrWhiteSpace(inspectorId))
            throw new ArgumentException("质检员ID不能为空", nameof(inspectorId));

        return new InventoryQuality
        {
            InventoryItemId = inventoryItemId,
            Status = QualityStatus.Pending,
            InspectorId = inspectorId,
            InspectionDate = DateTime.Now,
            Notes = notes
        };
    }

    public void AddDefect(
        string defectCode,
        string description,
        DefectSeverity severity)
    {
        if (Status != QualityStatus.Pending)
            throw new InvalidOperationException("只有待检状态的质检记录可以添加缺陷");

        if (string.IsNullOrWhiteSpace(defectCode))
            throw new ArgumentException("缺陷代码不能为空", nameof(defectCode));

        var defect = new QualityDefect(
            defectCode,
            description,
            severity);

        _defects.Add(defect);
    }

    public void Complete(bool isQualified)
    {
        if (Status != QualityStatus.Pending)
            throw new InvalidOperationException("只有待检状态的质检记录可以完成检验");

        Status = isQualified ? QualityStatus.Qualified : QualityStatus.Unqualified;
    }

    public void Reject(string notes)
    {
        if (Status != QualityStatus.Pending)
            throw new InvalidOperationException("只有待检状态的质检记录可以拒检");

        Status = QualityStatus.Rejected;
        Notes = notes;
    }
}

public class QualityDefect
{
    public string DefectCode { get; private set; }
    public string Description { get; private set; }
    public DefectSeverity Severity { get; private set; }

    internal QualityDefect(
        string defectCode,
        string description,
        DefectSeverity severity)
    {
        DefectCode = defectCode;
        Description = description;
        Severity = severity;
    }
}

public enum QualityStatus
{
    Pending = 0,     // 待检
    Qualified = 1,    // 合格
    Unqualified = 2,  // 不合格
    Rejected = 3      // 拒检
}

public enum DefectSeverity
{
    Minor = 0,       // 轻微
    Major = 1,       // 严重
    Critical = 2     // 致命
}