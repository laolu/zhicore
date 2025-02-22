using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行进度事件
    /// </summary>
    public class OperationProgressEto
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
        /// 计划完成时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 当前进度（百分比）
        /// </summary>
        public decimal Progress { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedEndTime { get; set; }

        /// <summary>
        /// 进度更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 进度状态（正常/延迟/提前）
        /// </summary>
        public string ProgressStatus { get; set; }

        /// <summary>
        /// 延迟原因（如果有）
        /// </summary>
        public string DelayReason { get; set; }
    }
}