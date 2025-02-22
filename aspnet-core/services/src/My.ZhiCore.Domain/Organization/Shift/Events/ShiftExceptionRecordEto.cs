using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 异常记录创建事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftExceptionRecordCreated")]
    public class ShiftExceptionRecordCreatedEto
    {
        /// <summary>异常记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>异常类型</summary>
        public string ExceptionType { get; set; }

        /// <summary>异常描述</summary>
        public string Description { get; set; }

        /// <summary>额外属性</summary>
        public Dictionary<string, string> ExtraProperties { get; set; }

        /// <summary>记录时间</summary>
        public DateTime RecordTime { get; set; }
    }

    /// <summary>
    /// 异常处理事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftExceptionHandled")]
    public class ShiftExceptionHandledEto
    {
        /// <summary>异常记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>处理结果</summary>
        public string HandlingResult { get; set; }

        /// <summary>处理时间</summary>
        public DateTime HandlingTime { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }
}