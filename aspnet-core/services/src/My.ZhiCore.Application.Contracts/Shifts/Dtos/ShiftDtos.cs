using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Shifts.Dtos
{
    /// <summary>
    /// 班次基础DTO
    /// </summary>
    public class ShiftBaseDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 班次编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// 班次类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否跨天
        /// </summary>
        public bool IsOvernight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建班次DTO
    /// </summary>
    public class CreateShiftDto
    {
        /// <summary>
        /// 班次编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// 班次类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否跨天
        /// </summary>
        public bool IsOvernight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新班次DTO
    /// </summary>
    public class UpdateShiftDto
    {
        /// <summary>
        /// 班次名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// 班次类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否跨天
        /// </summary>
        public bool IsOvernight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 班次考勤DTO
    /// </summary>
    public class ShiftAttendanceDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建班次考勤DTO
    /// </summary>
    public class CreateShiftAttendanceDto
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新班次考勤DTO
    /// </summary>
    public class UpdateShiftAttendanceDto
    {
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 班次交接DTO
    /// </summary>
    public class ShiftHandoverDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; set; }

        /// <summary>
        /// 交接时间
        /// </summary>
        public DateTime HandoverTime { get; set; }

        /// <summary>
        /// 交接人ID
        /// </summary>
        public Guid HandoverId { get; set; }

        /// <summary>
        /// 接班人ID
        /// </summary>
        public Guid TakeoverId { get; set; }

        /// <summary>
        /// 交接内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 交接状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建班次交接DTO
    /// </summary>
    public class CreateShiftHandoverDto
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; set; }

        /// <summary>
        /// 交接时间
        /// </summary>
        public DateTime HandoverTime { get; set; }

        /// <summary>
        /// 交接人ID
        /// </summary>
        public Guid HandoverId { get; set; }

        /// <summary>
        /// 接班人ID
        /// </summary>
        public Guid TakeoverId { get; set; }

        /// <summary>
        /// 交接内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 交接状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新班次交接DTO
    /// </summary>
    public class UpdateShiftHandoverDto
    {
        /// <summary>
        /// 交接内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 交接状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}