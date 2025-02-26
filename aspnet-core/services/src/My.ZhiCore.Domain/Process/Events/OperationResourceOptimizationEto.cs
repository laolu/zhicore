using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源分配优化事件
    /// </summary>
    public class OperationResourceOptimizationEto
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
        /// 优化时间
        /// </summary>
        public DateTime OptimizationTime { get; set; }

        /// <summary>
        /// 当前资源利用率
        /// </summary>
        public decimal CurrentResourceUtilization { get; set; }

        /// <summary>
        /// 建议的资源分配方案
        /// </summary>
        public string SuggestedAllocation { get; set; }

        /// <summary>
        /// 预期改进效果
        /// </summary>
        public string ExpectedImprovement { get; set; }

        /// <summary>
        /// 优化建议的优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 实施成本评估
        /// </summary>
        public string ImplementationCost { get; set; }
    }
}