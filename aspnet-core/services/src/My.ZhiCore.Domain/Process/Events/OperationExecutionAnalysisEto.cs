using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行分析事件
    /// </summary>
    public class OperationExecutionAnalysisEto
    {
        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 执行ID
        /// </summary>
        public Guid ExecutionId { get; set; }

        /// <summary>
        /// 分析时间
        /// </summary>
        public DateTime AnalysisTime { get; set; }

        /// <summary>
        /// 分析结果描述
        /// </summary>
        public string AnalysisResult { get; set; }

        /// <summary>
        /// 性能指标评分
        /// </summary>
        public decimal PerformanceScore { get; set; }

        /// <summary>
        /// 优化建议
        /// </summary>
        public string OptimizationSuggestions { get; set; }

        /// <summary>
        /// 异常情况说明
        /// </summary>
        public string AnomalyDescription { get; set; }
    }
}