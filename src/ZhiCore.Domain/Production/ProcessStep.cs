using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProcessStep : AuditableEntity
{
    public string OperationCode { get; private set; }
    public string Name { get; private set; }
    public int Sequence { get; private set; }
    public decimal StandardTime { get; private set; }
    public string Workstation { get; private set; }
    public string QualityRequirements { get; private set; }
    public string EquipmentRequirements { get; private set; }
    public string OperationInstructions { get; private set; }
    public string Notes { get; private set; }

    private ProcessStep() { }

    public static ProcessStep Create(
        string operationCode,
        string name,
        int sequence,
        decimal standardTime,
        string qualityRequirements = null,
        string equipmentRequirements = null,
        string operationInstructions = null,
        string workstation = null,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(operationCode))
            throw new ArgumentException("工序编码不能为空", nameof(operationCode));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("工序名称不能为空", nameof(name));

        if (sequence <= 0)
            throw new ArgumentException("工序顺序必须大于0", nameof(sequence));

        if (standardTime <= 0)
            throw new ArgumentException("标准工时必须大于0", nameof(standardTime));

        return new ProcessStep
        {
            OperationCode = operationCode,
            Name = name,
            Sequence = sequence,
            StandardTime = standardTime,
            QualityRequirements = qualityRequirements,
            EquipmentRequirements = equipmentRequirements,
            OperationInstructions = operationInstructions,
            Workstation = workstation,
            Notes = notes
        };
    }

    public void UpdateStandardTime(decimal newTime)
    {
        if (newTime <= 0)
            throw new ArgumentException("标准工时必须大于0", nameof(newTime));

        StandardTime = newTime;
    }

    public void UpdateQualityRequirements(string newQualityRequirements)
    {
        QualityRequirements = newQualityRequirements;
    }

    public void UpdateEquipmentRequirements(string newEquipmentRequirements)
    {
        EquipmentRequirements = newEquipmentRequirements;
    }

    public void UpdateOperationInstructions(string newOperationInstructions)
    {
        OperationInstructions = newOperationInstructions;
    }

    public void UpdateWorkstation(string newWorkstation)
    {
        Workstation = newWorkstation;
    }

    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
    }
}