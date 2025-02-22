using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序创建事件
    /// </summary>
    public class OperationCreatedEto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}