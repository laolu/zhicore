using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行配置事件
    /// </summary>
    public class OperationExecutionConfigurationEto
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
        /// 配置项键
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置项名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置项分组
        /// </summary>
        public string ConfigGroup { get; set; }

        /// <summary>
        /// 原配置值
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// 新配置值
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

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}