using System;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 工单状态变更事件
    /// </summary>
    public class WorkOrderStatusChangedEto
    {
        public Guid WorkOrderId { get; set; }
        public WorkOrderStatus OldStatus { get; set; }
        public WorkOrderStatus NewStatus { get; set; }
        public DateTime ChangedTime { get; set; }
    }
}