using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产执行暂停事件
    /// </summary>
    [EventName("My.ZhiCore.Production.ExecutionPaused")]
    public class ProductionExecutionPausedEto
    {
        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 暂停原因
        /// </summary>
        public string PauseReason { get; set; }

        /// <summary>
        /// 暂停时间
        /// </summary>
        public DateTime PauseTime { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }
    }
}