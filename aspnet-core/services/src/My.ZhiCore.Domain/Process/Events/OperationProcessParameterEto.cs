using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序工艺参数事件
    /// </summary>
    public class OperationProcessParameterEto
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
        /// 参数ID
        /// </summary>
        public Guid ParameterId { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParameterType { get; set; }

        /// <summary>
        /// 参数单位
        /// </summary>
        public string ParameterUnit { get; set; }

        /// <summary>
        /// 原参数值
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// 新参数值
        /// </summary>
        public string NewValue { get; set; }

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
    }
}