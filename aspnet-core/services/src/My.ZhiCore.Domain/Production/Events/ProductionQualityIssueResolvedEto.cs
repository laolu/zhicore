using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 质量问题解决事件
    /// </summary>
    [EventName("My.ZhiCore.Production.QualityIssueResolved")]
    public class ProductionQualityIssueResolvedEto
    {
        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        public string Solution { get; set; }

        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime ResolveTime { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public Guid ResolvedById { get; set; }

        /// <summary>
        /// 预防措施
        /// </summary>
        public string PreventiveMeasures { get; set; }
    }
}