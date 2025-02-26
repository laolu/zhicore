using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 交接班记录创建事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftHandoverRecordCreated")]
    public class ShiftHandoverRecordCreatedEto
    {
        /// <summary>交接班记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>交班人ID</summary>
        public Guid HandoverUserId { get; set; }

        /// <summary>接班人ID</summary>
        public Guid TakeoverUserId { get; set; }

        /// <summary>交接班时间</summary>
        public DateTime HandoverTime { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 交接班确认事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftHandoverConfirmed")]
    public class ShiftHandoverConfirmedEto
    {
        /// <summary>交接班记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>确认时间</summary>
        public DateTime ConfirmTime { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }
}