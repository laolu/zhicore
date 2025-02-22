using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 班次异常记录事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftException")]
    public class ShiftExceptionEto
    {
        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>班次编码</summary>
        public string ShiftCode { get; set; }

        /// <summary>班次名称</summary>
        public string ShiftName { get; set; }

        /// <summary>异常类型</summary>
        public ShiftExceptionType ExceptionType { get; set; }

        /// <summary>异常发生时间</summary>
        public DateTime OccurredTime { get; set; }

        /// <summary>异常描述</summary>
        public string Description { get; set; }

        /// <summary>影响程度</summary>
        public ShiftExceptionSeverity Severity { get; set; }

        /// <summary>处理状态</summary>
        public ShiftExceptionStatus Status { get; set; }

        /// <summary>处理人ID</summary>
        public Guid? HandlerId { get; set; }

        /// <summary>处理时间</summary>
        public DateTime? HandleTime { get; set; }

        /// <summary>处理结果</summary>
        public string HandleResult { get; set; }
    }

    /// <summary>
    /// 班次异常类型
    /// </summary>
    public enum ShiftExceptionType
    {   
        /// <summary>设备故障</summary>
        EquipmentFailure = 1,

        /// <summary>人员缺勤</summary>
        PersonnelAbsence = 2,

        /// <summary>材料短缺</summary>
        MaterialShortage = 3,

        /// <summary>质量问题</summary>
        QualityIssue = 4,

        /// <summary>安全事故</summary>
        SafetyIncident = 5,

        /// <summary>其他</summary>
        Other = 99
    }

    /// <summary>
    /// 异常影响程度
    /// </summary>
    public enum ShiftExceptionSeverity
    {
        /// <summary>轻微</summary>
        Minor = 1,

        /// <summary>一般</summary>
        Moderate = 2,

        /// <summary>严重</summary>
        Severe = 3,

        /// <summary>致命</summary>
        Critical = 4
    }

    /// <summary>
    /// 异常处理状态
    /// </summary>
    public enum ShiftExceptionStatus
    {
        /// <summary>待处理</summary>
        Pending = 1,

        /// <summary>处理中</summary>
        Processing = 2,

        /// <summary>已处理</summary>
        Handled = 3,

        /// <summary>已关闭</summary>
        Closed = 4
    }
}