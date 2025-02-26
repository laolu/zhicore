using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量问题状态变更事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityIssueStatusChanged")]
    public class QualityIssueStatusChangedEto
    {
        /// <summary>质量问题ID</summary>
        public Guid Id { get; set; }

        /// <summary>新状态</summary>
        public string NewStatus { get; set; }

        /// <summary>原状态</summary>
        public string OldStatus { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }

        /// <summary>变更时间</summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>变更人ID</summary>
        public Guid ChangerId { get; set; }
    }
}