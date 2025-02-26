using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 班次变更事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftChanged")]
    public class ShiftChangedEto
    {
        /// <summary>班次ID</summary>
        public Guid Id { get; set; }

        /// <summary>班次编码</summary>
        public string Code { get; set; }

        /// <summary>班次名称</summary>
        public string Name { get; set; }

        /// <summary>开始时间</summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>结束时间</summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>变更时间</summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }
    }
}