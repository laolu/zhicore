using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量问题处理完成事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityIssueResolved")]
    public class QualityIssueResolvedEto
    {
        /// <summary>质量问题ID</summary>
        public Guid Id { get; set; }

        /// <summary>解决方案</summary>
        public string Solution { get; set; }

        /// <summary>处理结果</summary>
        public string Result { get; set; }

        /// <summary>处理时间</summary>
        public DateTime ResolveTime { get; set; }

        /// <summary>处理人ID</summary>
        public Guid ResolverId { get; set; }

        /// <summary>验证结果</summary>
        public string VerificationResult { get; set; }

        /// <summary>预防措施</summary>
        public string PreventiveMeasures { get; set; }
    }
}