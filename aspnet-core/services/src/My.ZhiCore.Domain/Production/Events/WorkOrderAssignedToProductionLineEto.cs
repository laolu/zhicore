using System;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 工单分配到生产线事件
    /// </summary>
    public class WorkOrderAssignedToProductionLineEto
    {
        public Guid ProductionLineId { get; set; }
        public Guid WorkOrderId { get; set; }
        public DateTime AssignedTime { get; set; }
    }
}