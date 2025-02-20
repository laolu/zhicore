namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验结果枚举
    /// </summary>
    public enum QualityInspectionResult
    {
        /// <summary>
        /// 待检验
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 合格
        /// </summary>
        Qualified = 1,

        /// <summary>
        /// 不合格
        /// </summary>
        Unqualified = 2,

        /// <summary>
        /// 让步接收
        /// </summary>
        Concession = 3,

        /// <summary>
        /// 作废
        /// </summary>
        Invalid = 4
    }
}