using System;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产计划状态变更事件
    /// </summary>
    public class ProductionPlanStatusChangedEto
    {
        public Guid ProductionPlanId { get; set; }
        public ProductionPlanStatus OldStatus { get; set; }
        public ProductionPlanStatus NewStatus { get; set; }
        public DateTime ChangedTime { get; set; }
    }
}