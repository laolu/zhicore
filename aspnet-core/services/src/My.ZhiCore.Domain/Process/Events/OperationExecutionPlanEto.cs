using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行计划事件
    /// </summary>
    public class OperationExecutionPlanEto
    {
        /// <summary>
        /// 工序执行ID
        /// </summary>
        public Guid ExecutionId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal PlannedQuantity { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// 变更人ID
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }
    }
}