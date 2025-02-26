using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产执行恢复事件
    /// </summary>
    [EventName("My.ZhiCore.Production.ExecutionResumed")]
    public class ProductionExecutionResumedEto
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
        /// 恢复时间
        /// </summary>
        public DateTime ResumeTime { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }
    }
}