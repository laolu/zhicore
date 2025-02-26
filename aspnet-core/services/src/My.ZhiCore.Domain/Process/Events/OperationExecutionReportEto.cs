using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行报告事件
    /// </summary>
    public class OperationExecutionReportEto
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
        /// 计划工时（分钟）
        /// </summary>
        public decimal PlannedDuration { get; set; }

        /// <summary>
        /// 实际工时（分钟）
        /// </summary>
        public decimal ActualDuration { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal PlannedQuantity { get; set; }

        /// <summary>
        /// 实际完成数量
        /// </summary>
        public decimal ActualQuantity { get; set; }

        /// <summary>
        /// 合格品数量
        /// </summary>
        public decimal QualifiedQuantity { get; set; }

        /// <summary>
        /// 不合格品数量
        /// </summary>
        public decimal UnqualifiedQuantity { get; set; }

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal QualificationRate { get; set; }

        /// <summary>
        /// 计划完成率
        /// </summary>
        public decimal CompletionRate { get; set; }

        /// <summary>
        /// 资源利用率
        /// </summary>
        public decimal ResourceUtilizationRate { get; set; }

        /// <summary>
        /// 停机时长（分钟）
        /// </summary>
        public decimal DowntimeDuration { get; set; }

        /// <summary>
        /// 异常次数
        /// </summary>
        public int AbnormalCount { get; set; }

        /// <summary>
        /// 报告生成时间
        /// </summary>
        public DateTime ReportTime { get; set; }

        /// <summary>
        /// 报告类型（实时/日报/周报/月报）
        /// </summary>
        public string ReportType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}