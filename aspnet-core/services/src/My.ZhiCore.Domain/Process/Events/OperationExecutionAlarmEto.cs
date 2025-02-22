using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行报警事件
    /// </summary>
    public class OperationExecutionAlarmEto
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
        /// 报警代码
        /// </summary>
        public string AlarmCode { get; set; }

        /// <summary>
        /// 报警类型（如：设备报警、质量报警、安全报警等）
        /// </summary>
        public string AlarmType { get; set; }

        /// <summary>
        /// 报警级别（如：提示、警告、严重等）
        /// </summary>
        public string AlarmLevel { get; set; }

        /// <summary>
        /// 报警描述
        /// </summary>
        public string AlarmDescription { get; set; }

        /// <summary>
        /// 报警发生时间
        /// </summary>
        public DateTime OccurredTime { get; set; }

        /// <summary>
        /// 报警解除时间
        /// </summary>
        public DateTime? ClearedTime { get; set; }

        /// <summary>
        /// 报警状态（如：激活、已清除等）
        /// </summary>
        public string AlarmStatus { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public Guid? ProcessorId { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        public string ProcessMeasures { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string ProcessResult { get; set; }
    }
}