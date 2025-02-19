using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProcessResource : AuditableEntity
{
    public int ProcessId { get; private set; }
    public ResourceType Type { get; private set; }
    public int ResourceId { get; private set; }
    public decimal RequiredQuantity { get; private set; }
    public string Notes { get; private set; }
    
    public DateTime? StartTime { get; private set; }    // 资源使用开始时间
    public DateTime? EndTime { get; private set; }      // 资源使用结束时间
    public int Priority { get; private set; }          // 资源使用优先级
    public int? AlternativeResourceId { get; private set; }  // 替代资源ID
    public string Preconditions { get; private set; }  // 资源使用前置条件
    
    private ProcessResource() { }
    
    public static ProcessResource Create(
        int processId,
        ResourceType type,
        int resourceId,
        decimal requiredQuantity,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int priority = 0,
        int? alternativeResourceId = null,
        string preconditions = null,
        string notes = null)
    {
        if (processId <= 0)
            throw new ArgumentException("工序ID必须大于0", nameof(processId));
            
        if (resourceId <= 0)
            throw new ArgumentException("资源ID必须大于0", nameof(resourceId));
            
        if (requiredQuantity <= 0)
            throw new ArgumentException("需求数量必须大于0", nameof(requiredQuantity));
            
        return new ProcessResource
        {
            ProcessId = processId,
            Type = type,
            ResourceId = resourceId,
            RequiredQuantity = requiredQuantity,
            Notes = notes,
            StartTime = startTime,
            EndTime = endTime,
            Priority = priority,
            AlternativeResourceId = alternativeResourceId,
            Preconditions = preconditions
        };
    }
    
    public void UpdateTimeConstraints(DateTime? startTime, DateTime? endTime)
    {
        if (startTime.HasValue && endTime.HasValue && startTime.Value >= endTime.Value)
            throw new ArgumentException("结束时间必须晚于开始时间");
            
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public void UpdatePriority(int priority)
    {
        if (priority < 0)
            throw new ArgumentException("优先级不能为负数", nameof(priority));
            
        Priority = priority;
    }
    
    public void UpdateAlternativeResource(int? alternativeResourceId)
    {
        if (alternativeResourceId.HasValue && alternativeResourceId.Value <= 0)
            throw new ArgumentException("替代资源ID必须大于0", nameof(alternativeResourceId));
            
        if (alternativeResourceId == ResourceId)
            throw new ArgumentException("替代资源不能是资源本身", nameof(alternativeResourceId));
            
        AlternativeResourceId = alternativeResourceId;
    }
    
    public void UpdatePreconditions(ResourcePrecondition preconditions)
    {
        Preconditions = preconditions;
    }
    
    public bool HasTimeConflict(DateTime startTime, DateTime endTime)
    {
        if (!StartTime.HasValue || !EndTime.HasValue)
            return false;
            
        return !(endTime <= StartTime.Value || startTime >= EndTime.Value);
    }
    }
    
    public void UpdateRequiredQuantity(decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("需求数量必须大于0", nameof(quantity));
            
        RequiredQuantity = quantity;
    }
}

public enum ResourceType
{
    Material = 1,    // 物料
    Equipment = 2,   // 设备
    Tooling = 3,     // 工装
    Labor = 4        // 人力
}