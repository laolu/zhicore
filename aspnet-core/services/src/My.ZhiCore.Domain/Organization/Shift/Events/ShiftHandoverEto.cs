using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 班次交接事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftHandover")]
    public class ShiftHandoverEto
    {
        /// <summary>交接班ID</summary>
        public Guid Id { get; set; }

        /// <summary>交班班次ID</summary>
        public Guid OutgoingShiftId { get; set; }

        /// <summary>接班班次ID</summary>
        public Guid IncomingShiftId { get; set; }

        /// <summary>交接时间</summary>
        public DateTime HandoverTime { get; set; }

        /// <summary>交接状态</summary>
        public HandoverStatus Status { get; set; }

        /// <summary>交接备注</summary>
        public string Remarks { get; set; }

        /// <summary>未完成事项</summary>
        public string UnfinishedTasks { get; set; }

        /// <summary>需要关注的问题</summary>
        public string Issues { get; set; }

        /// <summary>交班人ID</summary>
        public Guid OutgoingPersonnelId { get; set; }

        /// <summary>接班人ID</summary>
        public Guid IncomingPersonnelId { get; set; }
    }

    /// <summary>
    /// 交接班状态
    /// </summary>
    public enum HandoverStatus
    {
        /// <summary>待交接</summary>
        Pending = 1,

        /// <summary>已交接</summary>
        Completed = 2,

        /// <summary>异常</summary>
        Exception = 3
    }
}