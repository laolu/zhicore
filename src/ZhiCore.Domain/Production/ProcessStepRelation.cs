using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProcessStepRelation : AuditableEntity
{
    public Guid FromStepId { get; private set; }
    public Guid ToStepId { get; private set; }
    public RelationType RelationType { get; private set; }
    public string Notes { get; private set; }
    public ProcessStep FromStep { get; private set; }
    public ProcessStep ToStep { get; private set; }

    private ProcessStepRelation() { }

    public static ProcessStepRelation Create(
        ProcessStep fromStep,
        ProcessStep toStep,
        RelationType relationType,
        string notes = null)
    {
        if (fromStep == null)
            throw new ArgumentNullException(nameof(fromStep));

        if (toStep == null)
            throw new ArgumentNullException(nameof(toStep));

        if (fromStep.Id == toStep.Id)
            throw new ArgumentException("工序不能与自身建立关系");

        return new ProcessStepRelation
        {
            FromStepId = fromStep.Id,
            ToStepId = toStep.Id,
            RelationType = relationType,
            Notes = notes,
            FromStep = fromStep,
            ToStep = toStep
        };
    }

    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
    }
}

public enum RelationType
{
    /// <summary>
    /// 前置关系：FromStep必须在ToStep之前完成
    /// </summary>
    Predecessor = 1,

    /// <summary>
    /// 并行关系：FromStep可以与ToStep同时进行
    /// </summary>
    Parallel = 2,

    /// <summary>
    /// 互斥关系：FromStep与ToStep不能同时进行
    /// </summary>
    Mutually_Exclusive = 3
}