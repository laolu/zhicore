using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量问题评审事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityIssueReviewed")]
    public class QualityIssueReviewedEto
    {
        /// <summary>问题ID</summary>
        public Guid Id { get; set; }

        /// <summary>评审人ID</summary>
        public Guid ReviewerId { get; set; }

        /// <summary>评审时间</summary>
        public DateTime ReviewTime { get; set; }

        /// <summary>评审结果</summary>
        public string ReviewResult { get; set; }

        /// <summary>评审意见</summary>
        public string ReviewComments { get; set; }

        /// <summary>是否通过</summary>
        public bool IsApproved { get; set; }
    }
}