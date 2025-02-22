using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行评估事件
    /// </summary>
    public class OperationExecutionEvaluationEto
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
        /// 质量评分（0-100）
        /// </summary>
        public decimal QualityScore { get; set; }

        /// <summary>
        /// 效率评分（0-100）
        /// </summary>
        public decimal EfficiencyScore { get; set; }

        /// <summary>
        /// 成本评分（0-100）
        /// </summary>
        public decimal CostScore { get; set; }

        /// <summary>
        /// 安全评分（0-100）
        /// </summary>
        public decimal SafetyScore { get; set; }

        /// <summary>
        /// 综合评分（0-100）
        /// </summary>
        public decimal OverallScore { get; set; }

        /// <summary>
        /// 合格状态（是/否）
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime EvaluationTime { get; set; }

        /// <summary>
        /// 评估人ID
        /// </summary>
        public Guid? EvaluatorId { get; set; }

        /// <summary>
        /// 评估意见
        /// </summary>
        public string EvaluationOpinion { get; set; }

        /// <summary>
        /// 改进建议
        /// </summary>
        public string ImprovementSuggestions { get; set; }
    }
}