namespace My.ZhiCore.Production.Shift
{
    /// <summary>
    /// 班次状态
    /// </summary>
    public enum ShiftStatus
    {
        /// <summary>
        /// 已计划
        /// </summary>
        Planned = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 3
    }
}