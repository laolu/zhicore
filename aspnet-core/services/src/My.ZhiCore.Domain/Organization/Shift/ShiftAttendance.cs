using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Shift
{
    /// <summary>
    /// 班次考勤记录实体 - 用于记录员工的考勤情况
    /// </summary>
    public class ShiftAttendance : Entity<Guid>
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; private set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public Guid EmployeeId { get; private set; }

        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; private set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CheckInTime { get; private set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime? CheckOutTime { get; private set; }

        /// <summary>
        /// 是否迟到
        /// </summary>
        public bool IsLate { get; private set; }

        /// <summary>
        /// 是否早退
        /// </summary>
        public bool IsEarlyLeave { get; private set; }

        /// <summary>
        /// 是否缺勤
        /// </summary>
        public bool IsAbsent { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected ShiftAttendance() { }

        public ShiftAttendance(
            Guid id,
            Guid shiftId,
            Guid employeeId,
            DateTime attendanceDate,
            DateTime? checkInTime = null,
            DateTime? checkOutTime = null,
            bool isLate = false,
            bool isEarlyLeave = false,
            bool isAbsent = false,
            string remarks = null)
        {
            Id = id;
            ShiftId = shiftId;
            EmployeeId = employeeId;
            AttendanceDate = attendanceDate;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            IsLate = isLate;
            IsEarlyLeave = isEarlyLeave;
            IsAbsent = isAbsent;
            Remarks = remarks;
        }
    }
}