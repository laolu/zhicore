using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量检验计划创建事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityInspectionPlanCreated")]
    public class QualityInspectionPlanCreatedEto
    {
        /// <summary>计划ID</summary>
        public Guid Id { get; set; }

        /// <summary>计划名称</summary>
        public string Name { get; set; }

        /// <summary>计划类型</summary>
        public string PlanType { get; set; }

        /// <summary>计划开始时间</summary>
        public DateTime StartTime { get; set; }

        /// <summary>计划结束时间</summary>
        public DateTime EndTime { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreationTime { get; set; }

        /// <summary>创建人ID</summary>
        public Guid CreatorId { get; set; }

        /// <summary>额外属性</summary>
        public Dictionary<string, string> ExtraProperties { get; set; }
    }
}