using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产效率指标变更事件
    /// </summary>
    [EventName("My.ZhiCore.Production.EfficiencyIndicatorChanged")]
    public class EfficiencyIndicatorChangedEto
    {
        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// OEE（设备综合效率）
        /// </summary>
        public decimal OEE { get; set; }

        /// <summary>
        /// 设备可用率
        /// </summary>
        public decimal Availability { get; set; }

        /// <summary>
        /// 性能效率
        /// </summary>
        public decimal Performance { get; set; }

        /// <summary>
        /// 质量合格率
        /// </summary>
        public decimal Quality { get; set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StatisticsTime { get; set; }
    }
}