using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行优先级变更事件
    /// </summary>
    public class OperationExecutionPriorityChangedEto
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
        /// 原优先级
        /// </summary>
        public int OldPriority { get; set; }

        /// <summary>
        /// 新优先级
        /// </summary>
        public int NewPriority { get; set; }

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

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}