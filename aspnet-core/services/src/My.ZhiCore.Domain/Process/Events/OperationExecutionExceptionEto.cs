using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行异常事件
    /// </summary>
    public class OperationExecutionExceptionEto
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
        /// 异常代码
        /// </summary>
        public string ExceptionCode { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string ExceptionDescription { get; set; }

        /// <summary>
        /// 异常等级
        /// </summary>
        public string SeverityLevel { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurredTime { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public string ProcessStatus { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public Guid? ProcessorId { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }

        /// <summary>
        /// 处理备注
        /// </summary>
        public string ProcessRemarks { get; set; }
    }
}