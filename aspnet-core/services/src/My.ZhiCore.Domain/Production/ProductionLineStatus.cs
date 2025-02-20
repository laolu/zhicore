namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产线状态枚举
    /// </summary>
    public enum ProductionLineStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Idle = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        Running = 1,

        /// <summary>
        /// 维护中
        /// </summary>
        Maintenance = 2
    }
}