using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量问题验证事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityIssueVerified")]
    public class QualityIssueVerifiedEto
    {
        /// <summary>问题ID</summary>
        public Guid Id { get; set; }

        /// <summary>验证人ID</summary>
        public Guid VerifierId { get; set; }

        /// <summary>验证时间</summary>
        public DateTime VerificationTime { get; set; }

        /// <summary>验证结果</summary>
        public string VerificationResult { get; set; }

        /// <summary>验证说明</summary>
        public string VerificationNote { get; set; }

        /// <summary>是否通过验证</summary>
        public bool IsVerified { get; set; }

        /// <summary>未通过原因</summary>
        public string FailureReason { get; set; }
    }
}