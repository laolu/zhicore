using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产执行完成事件
    /// </summary>
    [EventName("My.ZhiCore.Production.ExecutionCompleted")]
    public class ProductionExecutionCompletedEto
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
        /// 完成数量
        /// </summary>
        public int CompletedQuantity { get; set; }

        /// <summary>
        /// 合格品数量
        /// </summary>
        public int QualifiedQuantity { get; set; }

        /// <summary>
        /// 不合格品数量
        /// </summary>
        public int UnqualifiedQuantity { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompletionTime { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }
    }
}