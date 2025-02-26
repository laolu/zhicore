using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量检验项创建事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityInspectionItemCreated")]
    public class QualityInspectionItemCreatedEto
    {
        /// <summary>检验项ID</summary>
        public Guid Id { get; set; }

        /// <summary>所属计划ID</summary>
        public Guid PlanId { get; set; }

        /// <summary>检验项名称</summary>
        public string Name { get; set; }

        /// <summary>检验标准</summary>
        public string Standard { get; set; }

        /// <summary>检验方法</summary>
        public string Method { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreationTime { get; set; }

        /// <summary>创建人ID</summary>
        public Guid CreatorId { get; set; }

        /// <summary>额外属性</summary>
        public Dictionary<string, string> ExtraProperties { get; set; }
    }
}