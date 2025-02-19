using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class WorkShift : AuditableEntity
{
    public string ShiftCode { get; private set; }
    public string Name { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public bool IsActive { get; private set; }
    public string Notes { get; private set; }
    
    private readonly List<ShiftAssignment> _assignments = new();
    public IReadOnlyCollection<ShiftAssignment> Assignments => _assignments.AsReadOnly();

    private WorkShift() { }

    public static WorkShift Create(
        string shiftCode,
        string name,
        TimeSpan startTime,
        TimeSpan endTime,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(shiftCode))
            throw new ArgumentException("班次编号不能为空", nameof(shiftCode));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("班次名称不能为空", nameof(name));

        if (startTime == endTime)
            throw new ArgumentException("开始时间不能等于结束时间", nameof(endTime));

        return new WorkShift
        {
            ShiftCode = shiftCode,
            Name = name,
            StartTime = startTime,
            EndTime = endTime,
            IsActive = true,
            Notes = notes
        };
    }

    public void AddAssignment(
        int employeeId,
        DateTime assignmentDate,
        string notes = null)
    {
        if (!IsActive)
            throw new InvalidOperationException("不能为非活动班次添加排班");

        var assignment = ShiftAssignment.Create(
            employeeId,
            assignmentDate,
            notes);

        _assignments.Add(assignment);
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}

public class ShiftAssignment : AuditableEntity
{
    public int EmployeeId { get; private set; }
    public DateTime AssignmentDate { get; private set; }
    public AttendanceStatus Status { get; private set; }
    public string Notes { get; private set; }

    private ShiftAssignment() { }

    public static ShiftAssignment Create(
        int employeeId,
        DateTime assignmentDate,
        string notes = null)
    {
        if (employeeId <= 0)
            throw new ArgumentException("员工ID必须大于0", nameof(employeeId));

        return new ShiftAssignment
        {
            EmployeeId = employeeId,
            AssignmentDate = assignmentDate,
            Status = AttendanceStatus.Scheduled,
            Notes = notes
        };
    }

    public void UpdateStatus(AttendanceStatus newStatus)
    {
        Status = newStatus;
    }
}

public enum AttendanceStatus
{
    Scheduled = 1,   // 已排班
    Present = 2,     // 出勤
    Late = 3,        // 迟到
    Absent = 4,      // 缺勤
    Leave = 5        // 请假
}