using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序参数变更事件
    /// </summary>
    public class OperationParameterChangedEto
    {
        public Guid OperationId { get; set; }
        public Guid ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangeTime { get; set; }
    }
}