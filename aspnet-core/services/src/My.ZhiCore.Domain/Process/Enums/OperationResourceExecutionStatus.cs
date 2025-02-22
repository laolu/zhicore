namespace My.ZhiCore.Process
{
    /// <summary>
    /// 资源使用状态
    /// </summary>
    public enum ResourceExecutionStatus
    {
        /// <summary>
        /// 进行中
        /// </summary>
        InProgress = 0,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 1,

        /// <summary>
        /// 已中止
        /// </summary>
        Aborted = 2
    }
}