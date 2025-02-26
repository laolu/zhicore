using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量问题分配事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityIssueAssigned")]
    public class QualityIssueAssignedEto
    {
        /// <summary>问题ID</summary>
        public Guid Id { get; set; }

        /// <summary>被分配人ID</summary>
        public Guid AssigneeId { get; set; }

        /// <summary>分配人ID</summary>
        public Guid AssignerId { get; set; }

        /// <summary>分配时间</summary>
        public DateTime AssignTime { get; set; }

        /// <summary>期望完成时间</summary>
        public DateTime? ExpectedCompletionTime { get; set; }

        /// <summary>分配说明</summary>
        public string AssignmentNote { get; set; }
    }
}