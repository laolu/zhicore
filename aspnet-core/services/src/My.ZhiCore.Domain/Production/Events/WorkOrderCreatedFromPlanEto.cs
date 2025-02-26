using System;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 从生产计划创建工单事件
    /// </summary>
    public class WorkOrderCreatedFromPlanEto
    {
        public Guid ProductionPlanId { get; set; }
        public Guid WorkOrderId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}