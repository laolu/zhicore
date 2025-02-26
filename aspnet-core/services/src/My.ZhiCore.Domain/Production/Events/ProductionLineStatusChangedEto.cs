using System;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产线状态变更事件
    /// </summary>
    public class ProductionLineStatusChangedEto
    {
        public Guid ProductionLineId { get; set; }
        public ProductionLineStatus OldStatus { get; set; }
        public ProductionLineStatus NewStatus { get; set; }
        public DateTime ChangedTime { get; set; }
    }
}