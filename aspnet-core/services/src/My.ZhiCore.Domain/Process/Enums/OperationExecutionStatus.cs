namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序执行状态
    /// </summary>
    public enum OperationExecutionStatus
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
        /// 失败
        /// </summary>
        Failed = 2
    }
}