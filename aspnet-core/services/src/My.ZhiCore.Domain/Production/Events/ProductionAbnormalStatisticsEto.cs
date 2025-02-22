using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 生产异常统计事件
    /// </summary>
    [EventName("My.ZhiCore.Production.AbnormalStatisticsUpdated")]
    public class ProductionAbnormalStatisticsEto
    {
        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 设备故障次数
        /// </summary>
        public int EquipmentFailureCount { get; set; }

        /// <summary>
        /// 质量异常次数
        /// </summary>
        public int QualityIssueCount { get; set; }

        /// <summary>
        /// 物料短缺次数
        /// </summary>
        public int MaterialShortageCount { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}