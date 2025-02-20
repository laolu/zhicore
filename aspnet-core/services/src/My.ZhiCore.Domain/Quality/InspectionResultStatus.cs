namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验结果状态枚举
    /// </summary>
    public enum InspectionResultStatus
    {
        /// <summary>
        /// 待检验
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 检验中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 合格
        /// </summary>
        Qualified = 2,

        /// <summary>
        /// 不合格
        /// </summary>
        Unqualified = 3,

        /// <summary>
        /// 让步接收
        /// </summary>
        ConditionallyAccepted = 4
    }
}