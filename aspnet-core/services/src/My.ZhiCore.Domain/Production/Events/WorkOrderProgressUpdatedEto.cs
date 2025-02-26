using System;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 工单进度更新事件
    /// </summary>
    public class WorkOrderProgressUpdatedEto
    {
        public Guid WorkOrderId { get; set; }
        public int CompletedQuantity { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}