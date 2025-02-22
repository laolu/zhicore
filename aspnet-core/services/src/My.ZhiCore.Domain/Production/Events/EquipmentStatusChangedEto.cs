using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 设备运行状态变更事件
    /// </summary>
    [EventName("My.ZhiCore.Production.EquipmentStatusChanged")]
    public class EquipmentStatusChangedEto
    {
        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 新状态
        /// </summary>
        public string NewStatus { get; set; }

        /// <summary>
        /// 原状态
        /// </summary>
        public string PreviousStatus { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }
    }
}