namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验类型枚举
    /// </summary>
    public enum InspectionType
    {
        /// <summary>
        /// 首检
        /// </summary>
        FirstArticle = 0,

        /// <summary>
        /// 巡检
        /// </summary>
        Patrol = 1,

        /// <summary>
        /// 完工检验
        /// </summary>
        Final = 2,

        /// <summary>
        /// 过程检验
        /// </summary>
        Process = 3,

        /// <summary>
        /// 抽样检验
        /// </summary>
        Sampling = 4
    }
}