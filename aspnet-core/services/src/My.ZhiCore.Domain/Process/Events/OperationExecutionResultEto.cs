using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行结果事件
    /// </summary>
    public class OperationExecutionResultEto
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
        /// 实际开始时间
        /// </summary>
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 实际产出数量
        /// </summary>
        public decimal? ActualQuantity { get; set; }

        /// <summary>
        /// 合格品数量
        /// </summary>
        public decimal? QualifiedQuantity { get; set; }

        /// <summary>
        /// 不合格品数量
        /// </summary>
        public decimal? UnqualifiedQuantity { get; set; }

        /// <summary>
        /// 执行结果（成功/失败）
        /// </summary>
        public string ExecutionResult { get; set; }

        /// <summary>
        /// 结果说明
        /// </summary>
        public string ResultDescription { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 记录人ID
        /// </summary>
        public Guid? OperatorId { get; set; }
    }
}