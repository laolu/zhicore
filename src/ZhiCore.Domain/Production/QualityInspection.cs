using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class QualityInspection : AuditableEntity
{
    public string InspectionNumber { get; private set; }
    public int ProductionTaskId { get; private set; }
    public string InspectorId { get; private set; }
    public DateTime InspectionTime { get; private set; }
    public InspectionResult Result { get; private set; }
    public string Comments { get; private set; }
    public List<InspectionItem> InspectionItems { get; private set; }

    private QualityInspection()
    {
        InspectionItems = new List<InspectionItem>();
    }

    public static QualityInspection Create(
        string inspectionNumber,
        int productionTaskId,
        string inspectorId)
    {
        if (string.IsNullOrWhiteSpace(inspectionNumber))
            throw new ArgumentException("检验编号不能为空", nameof(inspectionNumber));

        if (productionTaskId <= 0)
            throw new ArgumentException("生产任务ID必须大于0", nameof(productionTaskId));

        if (string.IsNullOrWhiteSpace(inspectorId))
            throw new ArgumentException("检验员ID不能为空", nameof(inspectorId));

        return new QualityInspection
        {
            InspectionNumber = inspectionNumber,
            ProductionTaskId = productionTaskId,
            InspectorId = inspectorId,
            InspectionTime = DateTime.Now,
            Result = InspectionResult.Pending
        };
    }

    public void AddInspectionItem(InspectionItem item)
    {
        if (Result != InspectionResult.Pending)
            throw new InvalidOperationException("只有待完成的检验才能添加检验项");

        InspectionItems.Add(item);
    }

    public void Complete(string comments = null)
    {
        if (Result != InspectionResult.Pending)
            throw new InvalidOperationException("检验已完成");

        if (!InspectionItems.Exists(i => i.Result == ItemResult.Failed))
        {
            Result = InspectionResult.Passed;
        }
        else
        {
            Result = InspectionResult.Failed;
        }

        Comments = comments;
    }
}

public class InspectionItem : Entity
{
    public string Name { get; private set; }
    public string Standard { get; private set; }
    public string ActualValue { get; private set; }
    public ItemResult Result { get; private set; }
    public string DefectReason { get; private set; }

    private InspectionItem() { }

    public static InspectionItem Create(
        string name,
        string standard,
        string actualValue,
        ItemResult result,
        string defectReason = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("检验项名称不能为空", nameof(name));

        if (string.IsNullOrWhiteSpace(standard))
            throw new ArgumentException("检验标准不能为空", nameof(standard));

        if (string.IsNullOrWhiteSpace(actualValue))
            throw new ArgumentException("实际值不能为空", nameof(actualValue));

        if (result == ItemResult.Failed && string.IsNullOrWhiteSpace(defectReason))
            throw new ArgumentException("不合格项必须填写不合格原因", nameof(defectReason));

        return new InspectionItem
        {
            Name = name,
            Standard = standard,
            ActualValue = actualValue,
            Result = result,
            DefectReason = defectReason
        };
    }
}

public enum InspectionResult
{
    Pending = 1, // 待完成
    Passed = 2,  // 合格
    Failed = 3   // 不合格
}

public enum ItemResult
{
    Passed = 1, // 合格
    Failed = 2  // 不合格
}