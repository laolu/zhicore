using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class OutsourcingProcess : AuditableEntity
{
    public string ProcessCode { get; private set; }
    public string ProcessName { get; private set; }
    public string Description { get; private set; }
    public string TechnicalRequirements { get; private set; }
    public string QualityStandards { get; private set; }
    public decimal StandardCost { get; private set; }
    public ProcessStatus Status { get; private set; }
    
    // 工艺参数
    public string ProcessParameters { get; private set; }
    
    // 质检要求
    public string InspectionRequirements { get; private set; }
    
    // 包装要求
    public string PackagingRequirements { get; private set; }
    
    private readonly List<OutsourcingProcessMaterial> _materials = new();
    public IReadOnlyCollection<OutsourcingProcessMaterial> Materials => _materials.AsReadOnly();

    private OutsourcingProcess() { }

    public static OutsourcingProcess Create(
        string processCode,
        string processName,
        string description,
        string technicalRequirements,
        string qualityStandards,
        decimal standardCost,
        string processParameters = null,
        string inspectionRequirements = null,
        string packagingRequirements = null)
    {
        if (string.IsNullOrWhiteSpace(processCode))
            throw new ArgumentException("工序编码不能为空", nameof(processCode));

        if (string.IsNullOrWhiteSpace(processName))
            throw new ArgumentException("工序名称不能为空", nameof(processName));

        if (standardCost < 0)
            throw new ArgumentException("标准成本不能为负数", nameof(standardCost));

        return new OutsourcingProcess
        {
            ProcessCode = processCode,
            ProcessName = processName,
            Description = description,
            TechnicalRequirements = technicalRequirements,
            QualityStandards = qualityStandards,
            StandardCost = standardCost,
            ProcessParameters = processParameters,
            InspectionRequirements = inspectionRequirements,
            PackagingRequirements = packagingRequirements,
            Status = ProcessStatus.Draft
        };
    }

    public void AddMaterial(
        int materialId,
        decimal quantity,
        MaterialType type,
        string notes = null)
    {
        var material = OutsourcingProcessMaterial.Create(
            materialId,
            quantity,
            type,
            notes);

        _materials.Add(material);
    }

    public void UpdateStatus(ProcessStatus newStatus)
    {
        if (newStatus == Status)
            return;

        ValidateStatusTransition(newStatus);
        Status = newStatus;
    }

    public void UpdateStandardCost(decimal newCost)
    {
        if (newCost < 0)
            throw new ArgumentException("标准成本不能为负数", nameof(newCost));

        StandardCost = newCost;
    }

    private void ValidateStatusTransition(ProcessStatus newStatus)
    {
        switch (Status)
        {
            case ProcessStatus.Draft when newStatus != ProcessStatus.Active:
                throw new InvalidOperationException("草稿状态只能转换为生效状态");
            case ProcessStatus.Active when newStatus != ProcessStatus.Inactive:
                throw new InvalidOperationException("生效状态只能转换为失效状态");
            case ProcessStatus.Inactive:
                throw new InvalidOperationException("失效状态不能转换为其他状态");
        }
    }
}

public enum ProcessStatus
{
    Draft = 1,    // 草稿
    Active = 2,   // 生效
    Inactive = 3  // 失效
}

public enum MaterialType
{
    RawMaterial = 1,      // 原材料
    Auxiliary = 2,        // 辅料
    Packaging = 3,        // 包装材料
    SemiFinished = 4      // 半成品
}