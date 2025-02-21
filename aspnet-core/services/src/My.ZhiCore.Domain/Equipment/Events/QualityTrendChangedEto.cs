using System;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 质量趋势变化事件
    /// </summary>
    public class QualityTrendChangedEto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 质量趋势
        /// </summary>
        public QualityTrend QualityTrend { get; set; }

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal QualifiedRate { get; set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StatisticsTime { get; set; }
    }
}