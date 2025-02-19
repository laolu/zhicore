using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ShiftDefinition : AuditableEntity
{
    public string Name { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public decimal StandardWorkHours { get; private set; }
    public bool IsActive { get; private set; }
    public string Description { get; private set; }
    
    private ShiftDefinition() { }
    
    public static ShiftDefinition Create(
        string name,
        TimeSpan startTime,
        TimeSpan endTime,
        decimal standardWorkHours,
        bool isActive = true,
        string description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("班次名称不能为空", nameof(name));
            
        if (startTime >= endTime && endTime != TimeSpan.Zero)
            throw new ArgumentException("结束时间必须晚于开始时间");
            
        if (standardWorkHours <= 0)
            throw new ArgumentException("标准工作时长必须大于0", nameof(standardWorkHours));
            
        return new ShiftDefinition
        {
            Name = name,
            StartTime = startTime,
            EndTime = endTime,
            StandardWorkHours = standardWorkHours,
            IsActive = isActive,
            Description = description
        };
    }
    
    public void UpdateShiftTimes(TimeSpan startTime, TimeSpan endTime, decimal standardWorkHours)
    {
        if (startTime >= endTime && endTime != TimeSpan.Zero)
            throw new ArgumentException("结束时间必须晚于开始时间");
            
        if (standardWorkHours <= 0)
            throw new ArgumentException("标准工作时长必须大于0", nameof(standardWorkHours));
            
        StartTime = startTime;
        EndTime = endTime;
        StandardWorkHours = standardWorkHours;
    }
    
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("班次名称不能为空", nameof(name));
            
        Name = name;
    }
    
    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
    
    public void UpdateDescription(string description)
    {
        Description = description;
    }
    
    public bool IsWithinShift(TimeSpan time)
    {
        if (EndTime > StartTime)
        {
            return time >= StartTime && time <= EndTime;
        }
        else if (EndTime < StartTime) // 跨天的班次
        {
            return time >= StartTime || time <= EndTime;
        }
        return false; // EndTime == StartTime 的情况认为是无效的班次时间
    }
}