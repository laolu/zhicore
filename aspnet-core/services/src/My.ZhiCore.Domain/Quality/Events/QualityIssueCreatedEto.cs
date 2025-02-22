using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量问题创建事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityIssueCreated")]
    public class QualityIssueCreatedEto
    {
        /// <summary>质量问题ID</summary>
        public Guid Id { get; set; }

        /// <summary>问题类型</summary>
        public string IssueType { get; set; }

        /// <summary>问题描述</summary>
        public string Description { get; set; }

        /// <summary>严重程度</summary>
        public string Severity { get; set; }

        /// <summary>发现时间</summary>
        public DateTime DiscoveryTime { get; set; }

        /// <summary>发现人ID</summary>
        public Guid DiscovererId { get; set; }

        /// <summary>相关产品/工序ID</summary>
        public Guid? RelatedItemId { get; set; }

        /// <summary>额外属性</summary>
        public Dictionary<string, string> ExtraProperties { get; set; }
    }
}