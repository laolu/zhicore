using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量检验计划完成事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityInspectionPlanCompleted")]
    public class QualityInspectionPlanCompletedEto
    {
        /// <summary>计划ID</summary>
        public Guid Id { get; set; }

        /// <summary>完成时间</summary>
        public DateTime CompletionTime { get; set; }

        /// <summary>完成人ID</summary>
        public Guid CompletorId { get; set; }

        /// <summary>完成结果</summary>
        public string Result { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }
}