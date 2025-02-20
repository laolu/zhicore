namespace My.ZhiCore.Production.WorkOrder
{
    /// <summary>
    /// 工单状态枚举
    /// </summary>
    public enum WorkOrderStatus
    {
        /// <summary>
        /// 已创建
        /// </summary>
        Created = 0,

        /// <summary>
        /// 生产中
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