namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 质量趋势枚举，用于表示质量指标的变化趋势
    /// </summary>
    /// <remarks>
    /// 该枚举用于标识质量指标随时间的变化趋势：
    /// - 上升：质量指标呈现改善趋势
    /// - 稳定：质量指标保持在稳定水平
    /// - 下降：质量指标呈现恶化趋势
    /// </remarks>
    public enum QualityTrend
    {
        /// <summary>
        /// 上升：质量指标呈现改善趋势
        /// </summary>
        Improving = 0,

        /// <summary>
        /// 稳定：质量指标保持在稳定水平
        /// </summary>
        Stable = 1,

        /// <summary>
        /// 下降：质量指标呈现恶化趋势
        /// </summary>
        Declining = 2
    }
}