using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行时间事件
    /// </summary>
    public class OperationExecutionTimeEto
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
        public DateTime ActualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime ActualEndTime { get; set; }

        /// <summary>
        /// 总执行时长（分钟）
        /// </summary>
        public decimal TotalDuration { get; set; }

        /// <summary>
        /// 有效工作时长（分钟）
        /// </summary>
        public decimal EffectiveWorkDuration { get; set; }

        /// <summary>
        /// 停机时长（分钟）
        /// </summary>
        public decimal DowntimeDuration { get; set; }

        /// <summary>
        /// 准备时长（分钟）
        /// </summary>
        public decimal SetupDuration { get; set; }

        /// <summary>
        /// 维护时长（分钟）
        /// </summary>
        public decimal MaintenanceDuration { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 记录人ID
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}