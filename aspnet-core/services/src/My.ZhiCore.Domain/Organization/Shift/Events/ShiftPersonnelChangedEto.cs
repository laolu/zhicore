using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 班次人员变更事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftPersonnelChanged")]
    public class ShiftPersonnelChangedEto
    {
        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>人员ID</summary>
        public Guid PersonnelId { get; set; }

        /// <summary>变更类型（添加/移除）</summary>
        public ShiftPersonnelChangeType ChangeType { get; set; }

        /// <summary>生效日期</summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }

        /// <summary>变更时间</summary>
        public DateTime ChangeTime { get; set; }
    }

    /// <summary>
    /// 班次人员变更类型
    /// </summary>
    public enum ShiftPersonnelChangeType
    {
        /// <summary>添加人员</summary>
        Added = 1,

        /// <summary>移除人员</summary>
        Removed = 2
    }
}