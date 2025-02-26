using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行追踪事件
    /// </summary>
    public class OperationExecutionTrackingEto
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
        /// 追踪时间
        /// </summary>
        public DateTime TrackingTime { get; set; }

        /// <summary>
        /// 执行节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public string NodeType { get; set; }

        /// <summary>
        /// 节点状态
        /// </summary>
        public string NodeStatus { get; set; }

        /// <summary>
        /// 执行路径标识
        /// </summary>
        public string PathIdentifier { get; set; }

        /// <summary>
        /// 执行上下文数据
        /// </summary>
        public string ContextData { get; set; }

        /// <summary>
        /// 关联的前置节点
        /// </summary>
        public string PreviousNodes { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        public long ExecutionDuration { get; set; }
    }
}