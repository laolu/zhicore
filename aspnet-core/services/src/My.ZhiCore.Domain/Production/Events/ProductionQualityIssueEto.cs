using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产质量问题事件
    /// </summary>
    [EventName("My.ZhiCore.Production.QualityIssueOccurred")]
    public class ProductionQualityIssueEto
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
        /// 质量问题类型
        /// </summary>
        public string IssueType { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 影响的产品数量
        /// </summary>
        public int AffectedQuantity { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurrenceTime { get; set; }

        /// <summary>
        /// 发现人ID
        /// </summary>
        public Guid DiscoveredById { get; set; }

        /// <summary>
        /// 严重程度（1-5级）
        /// </summary>
        public int SeverityLevel { get; set; }
    }
}