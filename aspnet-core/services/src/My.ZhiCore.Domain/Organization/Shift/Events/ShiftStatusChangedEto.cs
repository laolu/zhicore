using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 班次状态变更事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftStatusChanged")]
    public class ShiftStatusChangedEto
    {
        /// <summary>班次ID</summary>
        public Guid Id { get; set; }

        /// <summary>班次编码</summary>
        public string Code { get; set; }

        /// <summary>班次名称</summary>
        public string Name { get; set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; set; }

        /// <summary>状态变更时间</summary>
        public DateTime StatusChangedTime { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }
    }
}