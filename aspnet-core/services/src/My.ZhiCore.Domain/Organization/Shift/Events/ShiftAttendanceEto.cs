using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 班次考勤记录事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftAttendance")]
    public class ShiftAttendanceEto
    {
        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>人员ID</summary>
        public Guid PersonnelId { get; set; }

        /// <summary>考勤日期</summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>签到时间</summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>签退时间</summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>考勤状态</summary>
        public ShiftAttendanceStatus Status { get; set; }

        /// <summary>迟到时长（分钟）</summary>
        public int? LateMinutes { get; set; }

        /// <summary>早退时长（分钟）</summary>
        public int? EarlyLeaveMinutes { get; set; }

        /// <summary>加班时长（分钟）</summary>
        public int? OvertimeMinutes { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 考勤状态
    /// </summary>
    public enum ShiftAttendanceStatus
    {
        /// <summary>正常</summary>
        Normal = 1,

        /// <summary>迟到</summary>
        Late = 2,

        /// <summary>早退</summary>
        EarlyLeave = 3,

        /// <summary>缺勤</summary>
        Absent = 4,

        /// <summary>请假</summary>
        Leave = 5,

        /// <summary>加班</summary>
        Overtime = 6
    }
}