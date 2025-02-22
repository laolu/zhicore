using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行调度事件
    /// </summary>
    public class OperationExecutionScheduleEto
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
        /// 调度类型（手动调度/自动调度）
        /// </summary>
        public string ScheduleType { get; set; }

        /// <summary>
        /// 调度时间
        /// </summary>
        public DateTime ScheduleTime { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 调度优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 调度人ID
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 调度原因
        /// </summary>
        public string ScheduleReason { get; set; }

        /// <summary>
        /// 调度备注
        /// </summary>
        public string Remarks { get; set; }
    }
}