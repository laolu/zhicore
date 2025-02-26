using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行完成事件
    /// </summary>
    public class OperationExecutionCompletedEto
    {
        public Guid Id { get; set; }
        public Guid OperationId { get; set; }
        public string ExecutionCode { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}