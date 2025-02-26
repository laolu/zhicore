using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 考勤记录创建事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftAttendanceRecordCreated")]
    public class ShiftAttendanceRecordCreatedEto
    {
        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>人员ID</summary>
        public Guid PersonnelId { get; set; }

        /// <summary>记录时间</summary>
        public DateTime RecordTime { get; set; }

        /// <summary>考勤类型</summary>
        public AttendanceType AttendanceType { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 考勤异常处理事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftAttendanceExceptionHandled")]
    public class ShiftAttendanceExceptionHandledEto
    {
        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>考勤记录ID</summary>
        public Guid AttendanceRecordId { get; set; }

        /// <summary>处理结果</summary>
        public string HandlingResult { get; set; }

        /// <summary>处理时间</summary>
        public DateTime HandlingTime { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 考勤类型
    /// </summary>
    public enum AttendanceType
    {
        /// <summary>签到</summary>
        SignIn = 1,

        /// <summary>签退</summary>
        SignOut = 2
    }
}