using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行监控事件
    /// </summary>
    public class OperationExecutionMonitoringEto
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
        /// 当前状态
        /// </summary>
        public string CurrentStatus { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal PlannedQuantity { get; set; }

        /// <summary>
        /// 已完成数量
        /// </summary>
        public decimal CompletedQuantity { get; set; }

        /// <summary>
        /// 合格品数量
        /// </summary>
        public decimal QualifiedQuantity { get; set; }

        /// <summary>
        /// 不合格品数量
        /// </summary>
        public decimal UnqualifiedQuantity { get; set; }

        /// <summary>
        /// 进度百分比
        /// </summary>
        public decimal ProgressPercentage { get; set; }

        /// <summary>
        /// 监控时间
        /// </summary>
        public DateTime MonitoringTime { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string AbnormalInfo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}