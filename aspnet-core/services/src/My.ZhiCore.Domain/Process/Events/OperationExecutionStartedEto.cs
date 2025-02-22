using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行开始事件
    /// </summary>
    public class OperationExecutionStartedEto
    {
        public Guid Id { get; set; }
        public Guid OperationId { get; set; }
        public string ExecutionCode { get; set; }
        public DateTime StartTime { get; set; }
    }
}