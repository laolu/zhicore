using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量成本记录事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityCostRecorded")]
    public class QualityCostRecordedEto
    {
        /// <summary>成本记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>成本类型</summary>
        public string CostType { get; set; }

        /// <summary>成本金额</summary>
        public decimal Amount { get; set; }

        /// <summary>成本描述</summary>
        public string Description { get; set; }

        /// <summary>发生时间</summary>
        public DateTime OccurredTime { get; set; }

        /// <summary>相关质量问题ID</summary>
        public Guid? RelatedQualityIssueId { get; set; }

        /// <summary>记录人ID</summary>
        public Guid RecorderId { get; set; }

        /// <summary>额外属性</summary>
        public Dictionary<string, string> ExtraProperties { get; set; }
    }
}